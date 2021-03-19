using Microsoft.EntityFrameworkCore;
using SkateboardNeverDie.Domain.Skaters;
using SkateboardNeverDie.Domain.Stances;
using SkateboardNeverDie.Domain.Tricks;

namespace SkateboardNeverDie.Infrastructure.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Skater> Skaters { get; set; }
        public DbSet<Stance> Stances { get; set; }
        public DbSet<Trick> Tricks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}
