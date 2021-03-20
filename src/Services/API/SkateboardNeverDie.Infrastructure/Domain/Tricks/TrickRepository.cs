using SkateboardNeverDie.Core.Domain;
using SkateboardNeverDie.Core.Infrastructure.Extensions;
using SkateboardNeverDie.Domain.Tricks;
using SkateboardNeverDie.Domain.Tricks.QueryData;
using SkateboardNeverDie.Infrastructure.Database;
using System;
using System.Threading.Tasks;

namespace SkateboardNeverDie.Infrastructure.Domain.Tricks
{
    public sealed class TrickRepository : ITrickRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public TrickRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        public async Task AddAsync(Trick trick)
        {
            await _applicationDbContext.Tricks.AddAsync(trick);
        }

        public async Task<PagedResult<TrickQueryData>> GetAllAsync(int page, int pageSize)
        {
            return await _applicationDbContext.Tricks.GetPagedResultAsync(
                page,
                pageSize,
                _ => new TrickQueryData
                {
                    Id = _.Id,
                    Name = _.Name
                });
        }
    }
}
