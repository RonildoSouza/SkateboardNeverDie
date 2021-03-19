using Microsoft.EntityFrameworkCore;
using SkateboardNeverDie.Domain.Stances;
using SkateboardNeverDie.Infrastructure.Database;
using System;

namespace SkateboardNeverDie.Infrastructure.Domain.Stances
{
    public sealed class StanceRepository : IStanceRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public StanceRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        public DbSet<Stance> Stances => _applicationDbContext.Stances;
    }
}
