using SkateboardNeverDie.Application.Tricks.Dtos;
using SkateboardNeverDie.Core.Application;
using System.Threading.Tasks;

namespace SkateboardNeverDie.Application.Tricks
{
    public interface ITrickAppService
    {
        Task<PagedResult<TrickDto>> GetAllAsync(int page, int pageSize);
    }
}
