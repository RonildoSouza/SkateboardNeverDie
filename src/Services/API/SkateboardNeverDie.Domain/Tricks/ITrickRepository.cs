using SkateboardNeverDie.Core.Domain;
using SkateboardNeverDie.Domain.Tricks.QueryData;
using System.Threading.Tasks;

namespace SkateboardNeverDie.Domain.Tricks
{
    public interface ITrickRepository
    {
        Task AddAsync(Trick trick);
        Task<PagedResult<TrickQueryData>> GetAllAsync(int page, int pageSize);
    }
}
