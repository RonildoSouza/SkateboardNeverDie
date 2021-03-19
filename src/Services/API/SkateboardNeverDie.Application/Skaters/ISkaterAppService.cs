using SkateboardNeverDie.Application.Skaters.Dtos;
using SkateboardNeverDie.Core.Application;
using System;
using System.Threading.Tasks;

namespace SkateboardNeverDie.Application.Skaters
{
    public interface ISkaterAppService
    {
        Task<PagedResult<SkaterDto>> GetAllAsync(int page, int pageSize);
        Task<SkaterDto> GetByIdAsync(Guid id);
        Task<SkaterDto> CreateSkaterAsync(CreateSkaterDto createSkaterDto);
    }
}
