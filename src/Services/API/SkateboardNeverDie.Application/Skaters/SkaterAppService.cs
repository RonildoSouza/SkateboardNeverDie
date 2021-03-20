using FluentValidation;
using SkateboardNeverDie.Application.Skaters.Dtos;
using SkateboardNeverDie.Application.Skaters.Validations;
using SkateboardNeverDie.Core.Domain;
using SkateboardNeverDie.Domain.Skaters;
using SkateboardNeverDie.Domain.Skaters.QueryData;
using System;
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

        public async Task<SkaterQueryData> GetByIdAsync(Guid id)
        {
            return await _skaterRepository.GetByIdAsync(id);
        }

        public async Task<PagedResult<SkaterQueryData>> GetAllAsync(int page, int pageSize)
        {
            return await _skaterRepository.GetAllAsync(page, pageSize);
        }

        public async Task<SkaterQueryData> CreateAsync(CreateSkaterDto createSkaterDto)
        {
            await new CreateSkaterValidator().ValidateAndThrowAsync(createSkaterDto);

            var skater = new Skater(
                createSkaterDto.FirstName,
                createSkaterDto.LastName,
                createSkaterDto.Nickname,
                createSkaterDto.Birthdate,
                createSkaterDto.NaturalStance);

            foreach (var skaterTrick in createSkaterDto.SkaterTricks)
                skater.AddTrick(skaterTrick.TrickId, skaterTrick.Variations);

            await _skaterRepository.AddAsync(skater);

            if (await _unitOfWork.CommitAsync())
            {
                return new SkaterQueryData
                {
                    Id = skater.Id,
                    FirstName = skater.FirstName,
                    LastName = skater.LastName,
                    Nickname = skater.Nickname,
                    Birthdate = skater.Birthdate,
                    NaturalStance = skater.NaturalStanceId
                };
            }

            return null;
        }
    }
}
