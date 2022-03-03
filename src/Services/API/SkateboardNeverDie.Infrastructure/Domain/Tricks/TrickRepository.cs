using Microsoft.EntityFrameworkCore;
using SkateboardNeverDie.Core.Domain;
using SkateboardNeverDie.Core.Infrastructure;
using SkateboardNeverDie.Core.Infrastructure.Extensions;
using SkateboardNeverDie.Domain.Tricks;
using SkateboardNeverDie.Domain.Tricks.QueryData;
using SkateboardNeverDie.Infrastructure.Database;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SkateboardNeverDie.Infrastructure.Domain.Tricks
{
    public sealed class TrickRepository : Repository<Trick, ApplicationDbContext>, ITrickRepository
    {
        public TrickRepository(ApplicationDbContext context)
            : base(context) { }

        public async Task AddAsync(Trick trick, CancellationToken cancelationToken)
        {
            await Context.Tricks.AddAsync(trick, cancelationToken);
        }

        public async Task<PagedResult<TrickQueryData>> GetAllAsync(int page, int pageSize, CancellationToken cancelationToken)
        {
            return await Context.Tricks.GetPagedResultAsync(
                page,
                pageSize,
                _ => new TrickQueryData
                {
                    Id = _.Id,
                    Name = _.Name,
                    Description = _.Description
                },
                _ => _.Name,
                cancelationToken);
        }

        public async Task<TrickQueryData> GetByIdAsync(Guid id, CancellationToken cancelationToken)
        {
            return await Context.Tricks.AsNoTracking()
                .Where(_ => _.Id == id)
                .Select(_ => new TrickQueryData
                {
                    Id = _.Id,
                    Name = _.Name,
                    Description = _.Description
                })
                .FirstOrDefaultAsync(cancelationToken);
        }

        public void Delete(Guid id)
        {
            try
            {
                var trick = new Trick(id);
                Context.Tricks.Attach(trick);
                Context.Tricks.Remove(trick);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> GetCountAsync(CancellationToken cancelationToken = default)
            => await Context.Tricks.AsNoTracking().CountAsync(cancelationToken);
    }
}
