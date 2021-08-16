using SkateboardNeverDie.Application.Tricks.Dtos;
using SkateboardNeverDie.Core.Domain;
using SkateboardNeverDie.Domain.Tricks.QueryData;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SkateboardNeverDie.Application.Tricks
{
    public interface ITrickAppService
    {
        Task<TrickQueryData> GetByIdAsync(Guid id, CancellationToken cancelationToken = default);
        Task<PagedResult<TrickQueryData>> GetAllAsync(int page, int pageSize, CancellationToken cancelationToken = default);
        Task<TrickQueryData> CreateAsync(CreateTrickDto createTrickDto, CancellationToken cancelationToken = default);
    }
}
