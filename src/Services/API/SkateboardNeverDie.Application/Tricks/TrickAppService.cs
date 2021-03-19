using SkateboardNeverDie.Application.Tricks.Dtos;
using SkateboardNeverDie.Core.Application;
using SkateboardNeverDie.Core.Domain;
using SkateboardNeverDie.Core.Infrastructure.Extensions;
using SkateboardNeverDie.Domain.Tricks;
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

        public async Task<PagedResult<TrickDto>> GetAllAsync(int page, int pageSize)
        {
            return await _trickRepository.Tricks.GetPagedResultAsync(
                page,
                pageSize,
                _ => new TrickDto
                {
                    Id = _.Id,
                    Name = _.Name
                });
        }
    }
}
