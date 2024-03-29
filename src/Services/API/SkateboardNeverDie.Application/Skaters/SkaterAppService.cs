﻿using FluentValidation;
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
        private readonly ISkaterRepository _skaterRepository;

        public SkaterAppService(ISkaterRepository skaterRepository)
        {
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

            if (await _skaterRepository.UnitOfWork.CommitAsync(cancelationToken))
                return SkaterQueryData.Convert(skater);

            return null;
        }

        public async Task<PagedResult<SkaterTrickQueryData>> GetSkaterTricksAsync(Guid skaterId, int page, int pageSize, CancellationToken cancelationToken)
        {
            return await _skaterRepository.GetSkaterTricksAsync(skaterId, page, pageSize, cancelationToken);
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancelationToken)
        {
            bool result;
            try
            {
                _skaterRepository.Delete(id);
                result = await _skaterRepository.UnitOfWork.CommitAsync(cancelationToken);
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }
    }
}
