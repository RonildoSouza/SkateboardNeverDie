using SkateboardNeverDie.Domain.Skaters;
using SkateboardNeverDie.Domain.Skaters.QueryData;
using SkateboardNeverDie.Domain.Stances;
using SkateboardNeverDie.Domain.Tricks;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SkateboardNeverDie.Application.Dashboard
{
    public sealed class DashboardAppService : IDashboardAppService
    {
        private readonly ISkaterRepository _skaterRepository;
        private readonly ITrickRepository _trickRepository;

        public DashboardAppService(ISkaterRepository skaterRepository, ITrickRepository trickRepository)
        {
            _skaterRepository = skaterRepository;
            _trickRepository = trickRepository;
        }

        public async Task<int> GetTricksCountAsync(CancellationToken cancelationToken = default)
            => await _trickRepository.GetCountAsync(cancelationToken);

        public async Task<int> GetSkatersCountAsync(CancellationToken cancelationToken = default)
            => await _skaterRepository.GetCountAsync(cancelationToken);

        public async Task<IDictionary<StanceType, int>> GetGoofyVsRegularAsync(CancellationToken cancelationToken = default)
            => await _skaterRepository.GetGoofyVsRegularAsync(cancelationToken);

        public async Task<IList<SkaterCountPerAgeQueryData>> GetSkatersCountPerAgeAsync(CancellationToken cancelationToken = default)
            => await _skaterRepository.GetSkatersCountPerAgeAsync(cancelationToken);
    }
}
