using SkateboardNeverDie.Core.Domain;
using SkateboardNeverDie.Domain.Tricks.QueryData;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SkateboardNeverDie.Domain.Tricks
{
    public interface ITrickRepository
    {
        Task AddAsync(Trick trick, CancellationToken cancelationToken = default);
        Task<PagedResult<TrickQueryData>> GetAllAsync(int page, int pageSize, CancellationToken cancelationToken = default);
        Task<TrickQueryData> GetByIdAsync(Guid id, CancellationToken cancelationToken = default);
    }
}
