using SkateboardNeverDie.Domain.Skaters.QueryData;
using SkateboardNeverDie.Domain.Stances;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SkateboardNeverDie.Application.Dashboard
{
    public interface IDashboardAppService
    {
        Task<int> GetTricksCountAsync(CancellationToken cancelationToken = default);
        Task<int> GetSkatersCountAsync(CancellationToken cancelationToken = default);
        Task<IDictionary<StanceType, int>> GetGoofyVsRegularAsync(CancellationToken cancelationToken = default);
        Task<IList<SkaterCountPerAgeQueryData>> GetSkatersCountPerAgeAsync(CancellationToken cancelationToken = default);
    }
}
