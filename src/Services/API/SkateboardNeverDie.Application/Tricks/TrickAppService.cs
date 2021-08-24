using FluentValidation;
using SkateboardNeverDie.Application.Tricks.Dtos;
using SkateboardNeverDie.Application.Tricks.Validations;
using SkateboardNeverDie.Core.Domain;
using SkateboardNeverDie.Domain.Tricks;
using SkateboardNeverDie.Domain.Tricks.QueryData;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SkateboardNeverDie.Application.Tricks
{
    public sealed class TrickAppService : ITrickAppService
    {
        private readonly ITrickRepository _trickRepository;

        public TrickAppService(ITrickRepository trickRepository)
        {
            _trickRepository = trickRepository;
        }

        public async Task<TrickQueryData> GetByIdAsync(Guid id, CancellationToken cancelationToken)
        {
            return await _trickRepository.GetByIdAsync(id, cancelationToken);
        }

        public async Task<PagedResult<TrickQueryData>> GetAllAsync(int page, int pageSize, CancellationToken cancelationToken)
        {
            return await _trickRepository.GetAllAsync(page, pageSize, cancelationToken);
        }

        public async Task<TrickQueryData> CreateAsync(CreateTrickDto createTrickDto, CancellationToken cancelationToken)
        {
            await new CreateTrickValidator().ValidateAndThrowAsync(createTrickDto, cancelationToken);

            var trick = new Trick(createTrickDto.Name, createTrickDto.Description);

            await _trickRepository.AddAsync(trick, cancelationToken);

            if (await _trickRepository.UnitOfWork.CommitAsync(cancelationToken))
                return TrickQueryData.Convert(trick);

            return null;
        }
    }
}
