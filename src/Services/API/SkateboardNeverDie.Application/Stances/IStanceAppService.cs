using SkateboardNeverDie.Application.Stances.Dtos;
using SkateboardNeverDie.Core.Application;
using System.Threading.Tasks;

namespace SkateboardNeverDie.Application.Stances
{
    public interface IStanceAppService
    {
        Task<PagedResult<StanceDto>> GetAllAsync(int page, int pageSize);
    }
}
