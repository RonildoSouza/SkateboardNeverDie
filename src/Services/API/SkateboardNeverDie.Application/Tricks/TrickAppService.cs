using SkateboardNeverDie.Core.Domain;
using SkateboardNeverDie.Domain.Tricks;
using SkateboardNeverDie.Domain.Tricks.QueryData;
using System.Threading.Tasks;

namespace SkateboardNeverDie.Application.Tricks
{

    public sealed class TrickAppService : ITrickAppService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITrickRepository _trickRepository;

        public TrickAppService(IUnitOfWork unitOfWork, ITrickRepository trickRepository)
        {
            _unitOfWork = unitOfWork;
            _trickRepository = trickRepository;
        }

        public async Task<PagedResult<TrickQueryData>> GetAllAsync(int page, int pageSize)
        {
            return await _trickRepository.GetAllAsync(page, pageSize);
        }
    }
}
