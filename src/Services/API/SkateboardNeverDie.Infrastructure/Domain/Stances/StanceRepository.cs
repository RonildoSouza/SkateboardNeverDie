using SkateboardNeverDie.Core.Domain;
using SkateboardNeverDie.Core.Infrastructure.Extensions;
using SkateboardNeverDie.Domain.Stances;
using SkateboardNeverDie.Domain.Stances.QueryData;
using SkateboardNeverDie.Infrastructure.Database;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SkateboardNeverDie.Infrastructure.Domain.Stances
{
    public sealed class StanceRepository : IStanceRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public StanceRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        public async Task<PagedResult<StanceQueryData>> GetAllAsync(int page, int pageSize, CancellationToken cancelationToken)
        {
            return await _applicationDbContext.Stances.GetPagedResultAsync(
                page,
                pageSize,
                _ => new StanceQueryData
                {
                    Id = _.Id,
                    Name = _.Id.ToString(),
                },
                cancelationToken);
        }
    }
}
