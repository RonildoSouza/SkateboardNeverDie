using Microsoft.EntityFrameworkCore;

namespace SkateboardNeverDie.Services.Auth.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
