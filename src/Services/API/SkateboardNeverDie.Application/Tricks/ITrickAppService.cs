using SkateboardNeverDie.Application.Tricks.Dtos;
using SkateboardNeverDie.Core.Domain;
using SkateboardNeverDie.Domain.Tricks.QueryData;
using System;
using System.Threading.Tasks;

namespace SkateboardNeverDie.Application.Tricks
{
    public interface ITrickAppService
    {
        Task<TrickQueryData> GetByIdAsync(Guid id);
        Task<PagedResult<TrickQueryData>> GetAllAsync(int page, int pageSize);
        Task<TrickQueryData> CreateAsync(CreateTrickDto createTrickDto);
    }
}
