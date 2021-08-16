using FluentValidation;
using SkateboardNeverDie.Application.Skaters.Dtos;
using SkateboardNeverDie.Application.Skaters.Validations;
using SkateboardNeverDie.Core.Domain;
using SkateboardNeverDie.Domain.Skaters;
using SkateboardNeverDie.Domain.Skaters.QueryData;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SkateboardNeverDie.Application.Skaters
{
    public sealed class SkaterAppService : ISkaterAppService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISkaterRepository _skaterRepository;

        public SkaterAppService(IUnitOfWork unitOfWork, ISkaterRepository skaterRepository)
        {
            _unitOfWork = unitOfWork;
            _skaterRepository = skaterRepository;
        }

        public async Task<SkaterQueryData> GetByIdAsync(Guid id, CancellationToken cancelationToken)
        {
            return await _skaterRepository.GetByIdAsync(id, cancelationToken);
        }

        public async Task<PagedResult<SkaterQueryData>> GetAllAsync(int page, int pageSize, CancellationToken cancelationToken)
        {
            return await _skaterRepository.GetAllAsync(page, pageSize, cancelationToken);
        }

        public async Task<SkaterQueryData> CreateAsync(CreateSkaterDto createSkaterDto, CancellationToken cancelationToken)
        {
            await new CreateSkaterValidator().ValidateAndThrowAsync(createSkaterDto, cancelationToken);

            var skater = new Skater(
                createSkaterDto.FirstName,
                createSkaterDto.LastName,
                createSkaterDto.Nickname,
                createSkaterDto.Birthdate,
                createSkaterDto.NaturalStance);

            foreach (var skaterTrick in createSkaterDto.SkaterTricks)
                skater.AddTrick(skaterTrick.TrickId, skaterTrick.Variations);

            await _skaterRepository.AddAsync(skater, cancelationToken);

            if (await _unitOfWork.CommitAsync(cancelationToken))
                return SkaterQueryData.Convert(skater);

            return null;
        }
    }
}
