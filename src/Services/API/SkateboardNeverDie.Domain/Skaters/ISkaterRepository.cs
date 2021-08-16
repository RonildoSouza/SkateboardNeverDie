﻿using SkateboardNeverDie.Core.Domain;
using SkateboardNeverDie.Domain.Skaters.QueryData;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SkateboardNeverDie.Domain.Skaters
{
    public interface ISkaterRepository
    {
        Task AddAsync(Skater skater, CancellationToken cancelationToken = default);
        Task<PagedResult<SkaterQueryData>> GetAllAsync(int page, int pageSize, CancellationToken cancelationToken = default);
        Task<SkaterQueryData> GetByIdAsync(Guid id, CancellationToken cancelationToken = default);
    }
}
