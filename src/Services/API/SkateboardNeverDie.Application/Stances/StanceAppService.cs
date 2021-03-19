using SkateboardNeverDie.Application.Stances.Dtos;
using SkateboardNeverDie.Core.Application;
using SkateboardNeverDie.Core.Domain;
using SkateboardNeverDie.Core.Infrastructure.Extensions;
using SkateboardNeverDie.Domain.Stances;
using System.Threading.Tasks;

namespace SkateboardNeverDie.Application.Stances
{
    public sealed class StanceAppService : IStanceAppService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStanceRepository _stanceRepository;

        public StanceAppService(IUnitOfWork unitOfWork, IStanceRepository skaterRepository)
        {
            _unitOfWork = unitOfWork;
            _stanceRepository = skaterRepository;
        }

        public async Task<PagedResult<StanceDto>> GetAllAsync(int page, int pageSize)
        {
            return await _stanceRepository.Stances.GetPagedResultAsync(
                page,
                pageSize,
                _ => new StanceDto
                {
                    Id = _.Id,
                    Name = _.Id.ToString()
                });
        }
    }
}
