using Microsoft.EntityFrameworkCore;
using SkateboardNeverDie.Domain.Tricks;
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

        public DbSet<Trick> Tricks => _applicationDbContext.Tricks;

        public async Task AddAsync(Trick trick)
        {
            await _applicationDbContext.Tricks.AddAsync(trick);
        }
    }
}
