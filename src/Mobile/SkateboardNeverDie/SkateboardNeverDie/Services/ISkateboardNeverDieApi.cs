using Refit;
using SkateboardNeverDie.Models;
using System;
using System.Threading.Tasks;

namespace SkateboardNeverDie.Services
{
    public interface ISkateboardNeverDieApi
    {
        [Get("/api/v1/skaters")]
        Task<HateoasResult<PagedResult<Skater>>> GetSkatersAsync([Query] int page = 1, [Query] int pageSize = 10);

        [Get("/api/v1/skaters/{id}")]
        Task<HateoasResult<Skater>> GetSkaterByIdAsync(Guid id);

        [Get("/api/v1/tricks")]
        Task<HateoasResult<PagedResult<Trick>>> GetTricksAsync([Query] int page = 1, [Query] int pageSize = 10);
    }
}
