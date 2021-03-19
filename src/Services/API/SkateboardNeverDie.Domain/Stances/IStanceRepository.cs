using Microsoft.EntityFrameworkCore;

namespace SkateboardNeverDie.Domain.Stances
{
    public interface IStanceRepository
    {
        public DbSet<Stance> Stances { get; }
    }
}
