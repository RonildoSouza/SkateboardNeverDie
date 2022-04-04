using Microsoft.EntityFrameworkCore;
using SkateboardNeverDie.Core.Domain;
using SkateboardNeverDie.Core.Infrastructure;
using SkateboardNeverDie.Core.Infrastructure.Extensions;
using SkateboardNeverDie.Domain.Skaters;
using SkateboardNeverDie.Domain.Skaters.QueryData;
using SkateboardNeverDie.Domain.Stances;
using SkateboardNeverDie.Infrastructure.Database;
using System;
using System.Collections.Generic;
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

        public async Task<PagedResult<SkaterTrickQueryData>> GetSkaterTricksAsync(Guid skaterId, int page, int pageSize, CancellationToken cancelationToken)
        {
            var query = Context.Skaters
                .Include(_ => _.SkaterTricks)
                .ThenInclude(_ => _.Trick)
                .Include(_ => _.SkaterTricks)
                .ThenInclude(_ => _.SkaterTrickVariations)
                .Where(_ => _.Id == skaterId)
                .SelectMany(s => s.SkaterTricks
                    .Select(st => new SkaterTrickQueryData
                    {
                        Id = st.Id,
                        TrickName = st.Trick.Name,
                        TrickVariations = st.SkaterTrickVariations.Select(stv => stv.StanceId).OrderBy(_ => _)
                    }));

            //return await query.GetPagedResultAsync(
            //    page,
            //    pageSize,
            //    _ => _,
            //    _ => _.TrickName,
            //    cancelationToken);

            return await query.ToList().GetPagedResultAsync(
                page,
                pageSize,
                _ => _,
                _ => _.TrickName);
        }

        public void Delete(Guid id)
        {
            try
            {
                var skater = new Skater(id);
                Context.Skaters.Attach(skater);
                Context.Skaters.Remove(skater);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> GetCountAsync(CancellationToken cancelationToken = default)
            => await Context.Skaters.AsNoTracking().CountAsync(cancelationToken);

        public async Task<IDictionary<StanceType, int>> GetGoofyVsRegularAsync(CancellationToken cancelationToken = default)
        {
            return await Context.Skaters
                .GroupBy(_ => _.NaturalStanceId)
                .Select(_ => new { _.Key, Value = _.Count() })
                .ToDictionaryAsync(_ => _.Key, _ => _.Value, cancelationToken);
        }

        public async Task<IList<SkaterCountPerAgeQueryData>> GetSkatersCountPerAgeAsync(CancellationToken cancelationToken = default)
        {
            return (await Context.Skaters
                .Select(_ => _.Birthdate)
                .ToListAsync())
                .GroupBy(_ => (DateTime.Today - _).Days / 365)
                .Select(_ => new SkaterCountPerAgeQueryData
                {
                    Count = _.Count(),
                    Age = _.Key
                })
                .ToList();
        }
    }
}
