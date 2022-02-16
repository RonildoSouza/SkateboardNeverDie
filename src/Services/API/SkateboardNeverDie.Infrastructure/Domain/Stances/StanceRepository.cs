using SkateboardNeverDie.Core.Domain;
using SkateboardNeverDie.Core.Infrastructure;
using SkateboardNeverDie.Core.Infrastructure.Extensions;
using SkateboardNeverDie.Domain.Stances;
using SkateboardNeverDie.Domain.Stances.QueryData;
using SkateboardNeverDie.Infrastructure.Database;
using System.Threading;
using System.Threading.Tasks;

namespace SkateboardNeverDie.Infrastructure.Domain.Stances
{
    public sealed class StanceRepository : Repository<Stance, ApplicationDbContext>, IStanceRepository
    {
        public StanceRepository(ApplicationDbContext context)
            : base(context) { }

        public async Task<PagedResult<StanceQueryData>> GetAllAsync(int page, int pageSize, CancellationToken cancelationToken)
        {
            return await Context.Stances.GetPagedResultAsync(
                page,
                pageSize,
                _ => new StanceQueryData
                {
                    Id = _.Id,
                    Description = _.Description,
                },
                cancelationToken);
        }
    }
}
