using Microsoft.EntityFrameworkCore;
using SkateboardNeverDie.Core.Domain;
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
    public sealed class SkaterRepository : ISkaterRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public SkaterRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        public async Task AddAsync(Skater skater, CancellationToken cancelationToken)
        {
            await _applicationDbContext.Skaters.AddAsync(skater, cancelationToken);
        }

        public async Task<PagedResult<SkaterQueryData>> GetAllAsync(int page, int pageSize, CancellationToken cancelationToken)
        {
            return await _applicationDbContext.Skaters.GetPagedResultAsync(
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
                cancelationToken);
        }

        public async Task<SkaterQueryData> GetByIdAsync(Guid id, CancellationToken cancelationToken)
        {
            return await _applicationDbContext.Skaters.AsNoTracking()
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
