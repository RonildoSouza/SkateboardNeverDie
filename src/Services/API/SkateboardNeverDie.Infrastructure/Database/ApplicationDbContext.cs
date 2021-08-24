using Microsoft.EntityFrameworkCore;
using SkateboardNeverDie.Core.Domain;
using SkateboardNeverDie.Domain.Security;
using SkateboardNeverDie.Domain.Skaters;
using SkateboardNeverDie.Domain.Stances;
using SkateboardNeverDie.Domain.Tricks;
using System.Threading;
using System.Threading.Tasks;

namespace SkateboardNeverDie.Infrastructure.Database
{
    public class ApplicationDbContext : DbContext, IUnitOfWork
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Skater> Skaters { get; set; }
        public DbSet<Stance> Stances { get; set; }
        public DbSet<Trick> Tricks { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<UserPermission> UserPermissions { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }

        public async Task<bool> CommitAsync(CancellationToken cancellationToken = default)
        {
            return await SaveChangesAsync(cancellationToken) > 0;
        }
    }
}
