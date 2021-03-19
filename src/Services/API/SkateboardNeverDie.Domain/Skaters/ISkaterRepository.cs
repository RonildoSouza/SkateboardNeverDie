using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace SkateboardNeverDie.Domain.Skaters
{
    public interface ISkaterRepository
    {
        public DbSet<Skater> Skaters { get; }

        Task AddAsync(Skater skater);
        Task<Skater> GetByIdAsync(Guid id);
    }
}
