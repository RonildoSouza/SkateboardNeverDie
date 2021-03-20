using SkateboardNeverDie.Core.Domain;
using SkateboardNeverDie.Domain.Stances;
using SkateboardNeverDie.Domain.Stances.QueryData;
using System.Threading.Tasks;

namespace SkateboardNeverDie.Application.Stances
{
    public sealed class StanceAppService : IStanceAppService
    {
        private readonly IStanceRepository _stanceRepository;

        public StanceAppService(IStanceRepository skaterRepository)
        {
            _stanceRepository = skaterRepository;
        }

        public async Task<PagedResult<StanceQueryData>> GetAllAsync(int page, int pageSize)
        {
            return await _stanceRepository.GetAllAsync(page, pageSize);
        }
    }
}
