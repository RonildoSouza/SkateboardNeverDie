using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace SkateboardNeverDie.Domain.Tricks
{
    public interface ITrickRepository
    {
        public DbSet<Trick> Tricks { get; }

        Task AddAsync(Trick trick);
    }
}
