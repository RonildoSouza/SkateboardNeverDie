using Microsoft.EntityFrameworkCore;
using SkateboardNeverDie.Core.Domain;
using SkateboardNeverDie.Core.Infrastructure;
using SkateboardNeverDie.Core.Infrastructure.Extensions;
using SkateboardNeverDie.Domain.Skaters;
using SkateboardNeverDie.Domain.Skaters.QueryData;
using SkateboardNeverDie.Infrastructure.Database;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SkateboardNeverDie.Infrastructure.Domain.Skaters
{
    public sealed class SkaterRepository : Repository<Skater, ApplicationDbContext>, ISkaterRepository
    {
        public SkaterRepository(ApplicationDbContext context)
            : base(context) { }

        public async Task AddAsync(Skater skater, CancellationToken cancelationToken)
        {
            await Context.Skaters.AddAsync(skater, cancelationToken);
        }

        public async Task<PagedResult<SkaterQueryData>> GetAllAsync(int page, int pageSize, CancellationToken cancelationToken)
        {
            return await Context.Skaters.GetPagedResultAsync(
                page,
                pageSize,
                _ => new SkaterQueryData
                {
                    Id = _.Id,
                    FirstName = _.FirstName,
                    LastName = _.LastName,
                    Nickname = _.Nickname,
                    Birthdate = _.Birthdate,
                    NaturalStance = _.NaturalStanceId
                },
                _ => _.FirstName,
                cancelationToken);
        }

        public async Task<SkaterQueryData> GetByIdAsync(Guid id, CancellationToken cancelationToken)
        {
            return await Context.Skaters.AsNoTracking()
                .Where(_ => _.Id == id)
                .Select(_ => new SkaterQueryData
                {
                    Id = _.Id,
                    FirstName = _.FirstName,
                    LastName = _.LastName,
                    Nickname = _.Nickname,
                    Birthdate = _.Birthdate,
                    NaturalStance = _.NaturalStanceId
                })
                .FirstOrDefaultAsync(cancelationToken);
        }
    }
}
