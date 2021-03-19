using FluentValidation;
using SkateboardNeverDie.Application.Skaters.Dtos;
using SkateboardNeverDie.Application.Skaters.Validations;
using SkateboardNeverDie.Core.Application;
using SkateboardNeverDie.Core.Domain;
using SkateboardNeverDie.Core.Infrastructure.Extensions;
using SkateboardNeverDie.Domain.Skaters;
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

        public async Task<PagedResult<SkaterDto>> GetAllAsync(int page, int pageSize)
        {
            return await _skaterRepository.Skaters.GetPagedResultAsync(
                page,
                pageSize,
                _ => new SkaterDto
                {
                    Id = _.Id,
                    FirstName = _.FirstName,
                    LastName = _.LastName,
                    Nickname = _.Nickname,
                    Birthdate = _.Birthdate,
                    NaturalStance = _.NaturalStanceId
                });
        }

        public async Task<SkaterDto> GetByIdAsync(Guid id)
        {
            var skater = await _skaterRepository.GetByIdAsync(id);

            if (skater == null)
                return null;

            return new SkaterDto
            {
                Id = skater.Id,
                FirstName = skater.FirstName,
                LastName = skater.LastName,
                Nickname = skater.Nickname,
                Birthdate = skater.Birthdate,
                NaturalStance = skater.NaturalStanceId
            };
        }

        public async Task<SkaterDto> CreateSkaterAsync(CreateSkaterDto createSkaterDto)
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
                return new SkaterDto
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
