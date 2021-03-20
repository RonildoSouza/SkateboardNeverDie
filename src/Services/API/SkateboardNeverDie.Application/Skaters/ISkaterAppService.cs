using SkateboardNeverDie.Application.Skaters.Dtos;
using SkateboardNeverDie.Core.Domain;
using SkateboardNeverDie.Domain.Skaters.QueryData;
using System;
using System.Threading.Tasks;

namespace SkateboardNeverDie.Application.Skaters
{
    public interface ISkaterAppService
    {
        Task<PagedResult<SkaterQueryData>> GetAllAsync(int page, int pageSize);
        Task<SkaterQueryData> GetByIdAsync(Guid id);
        Task<SkaterQueryData> CreateAsync(CreateSkaterDto createSkaterDto);
    }
}
