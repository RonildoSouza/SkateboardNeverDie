using FluentValidation;
using SkateboardNeverDie.Application.Tricks.Dtos;
using SkateboardNeverDie.Application.Tricks.Validations;
using SkateboardNeverDie.Core.Domain;
using SkateboardNeverDie.Domain.Tricks;
using SkateboardNeverDie.Domain.Tricks.QueryData;
using System.Threading.Tasks;

namespace SkateboardNeverDie.Application.Tricks
{

    public sealed class TrickAppService : ITrickAppService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITrickRepository _trickRepository;

        public TrickAppService(IUnitOfWork unitOfWork, ITrickRepository trickRepository)
        {
            _unitOfWork = unitOfWork;
            _trickRepository = trickRepository;
        }

        public async Task<PagedResult<TrickQueryData>> GetAllAsync(int page, int pageSize)
        {
            return await _trickRepository.GetAllAsync(page, pageSize);
        }

        public async Task<TrickQueryData> CreateAsync(CreateTrickDto createTrickDto)
        {
            await new CreateTrickValidator().ValidateAndThrowAsync(createTrickDto);

            var trick = new Trick(createTrickDto.Name, createTrickDto.Description);

            await _trickRepository.AddAsync(trick);

            if (await _unitOfWork.CommitAsync())
            {
                return new TrickQueryData
                {
                    Id = trick.Id,
                    Name = trick.Name
                };
            }

            return null;
        }
    }
}
