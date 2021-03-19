using Microsoft.EntityFrameworkCore;
using SkateboardNeverDie.Domain.Skaters;
using SkateboardNeverDie.Infrastructure.Database;
using System;
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

        public DbSet<Skater> Skaters => _applicationDbContext.Skaters;

        public async Task AddAsync(Skater skater)
        {
            await _applicationDbContext.Skaters.AddAsync(skater);
        }

        public async Task<Skater> GetByIdAsync(Guid id)
        {
            return await Skaters.AsNoTracking().FirstOrDefaultAsync(_ => _.Id == id);
        }
    }
}
