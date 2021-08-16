using SkateboardNeverDie.Core.Domain;
using SkateboardNeverDie.Domain.Stances.QueryData;
using System.Threading;
using System.Threading.Tasks;

namespace SkateboardNeverDie.Domain.Stances
{
    public interface IStanceRepository
    {
        Task<PagedResult<StanceQueryData>> GetAllAsync(int page, int pageSize, CancellationToken cancelationToken = default);
    }
}
