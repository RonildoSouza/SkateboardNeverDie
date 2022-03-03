using SkateboardNeverDie.Core.Domain;
using SkateboardNeverDie.Domain.Skaters.QueryData;
using SkateboardNeverDie.Domain.Stances;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SkateboardNeverDie.Domain.Skaters
{
    public interface ISkaterRepository : IRepository<Skater>
    {
        Task AddAsync(Skater skater, CancellationToken cancelationToken = default);
        Task<PagedResult<SkaterQueryData>> GetAllAsync(int page, int pageSize, CancellationToken cancelationToken = default);
        Task<SkaterQueryData> GetByIdAsync(Guid id, CancellationToken cancelationToken = default);
        Task<PagedResult<SkaterTrickQueryData>> GetSkaterTricksAsync(Guid skaterId, int page, int pageSize, CancellationToken cancelationToken = default);
        void Delete(Guid id);
        Task<int> GetCountAsync(CancellationToken cancelationToken = default);
        Task<IDictionary<StanceType, int>> GetGoofyVsRegularAsync(CancellationToken cancelationToken = default);
        Task<IList<SkaterCountPerAgeQueryData>> GetSkatersCountPerAgeAsync(CancellationToken cancelationToken = default);
    }
}
