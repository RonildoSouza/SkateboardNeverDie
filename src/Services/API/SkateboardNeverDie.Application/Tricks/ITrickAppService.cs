using SkateboardNeverDie.Core.Domain;
using SkateboardNeverDie.Domain.Tricks.QueryData;
using System.Threading.Tasks;

namespace SkateboardNeverDie.Application.Tricks
{
    public interface ITrickAppService
    {
        Task<PagedResult<TrickQueryData>> GetAllAsync(int page, int pageSize);
    }
}
