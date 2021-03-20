using SkateboardNeverDie.Core.Domain;
using SkateboardNeverDie.Domain.QueryData;
using System;
using System.Threading.Tasks;

namespace SkateboardNeverDie.Domain.Skaters
{
    public interface ISkaterRepository
    {
        Task AddAsync(Skater skater);
        Task<PagedResult<SkaterQueryData>> GetAllAsync(int page, int pageSize);
        Task<SkaterQueryData> GetByIdAsync(Guid id);
    }
}
