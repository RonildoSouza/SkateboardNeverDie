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

        [Post("/api/v1/skaters")]
        Task<HateoasResult<Skater>> PostSkatersAsync([Body] CreateSkater createSkater);

        [Get("/api/v1/skaters/{id}")]
        Task<HateoasResult<Skater>> GetSkaterByIdAsync(Guid id);

        [Get("/api/v1/skaters/{id}/tricks")]
        Task<HateoasResult<PagedResult<SkaterTrick>>> GetSkaterTricksAsync(Guid id, [Query] int page = 1, [Query] int pageSize = 10);

        [Get("/api/v1/tricks")]
        Task<HateoasResult<PagedResult<Trick>>> GetTricksAsync([Query] int page = 1, [Query] int pageSize = 10);

        [Post("/api/v1/tricks")]
        Task<HateoasResult<Trick>> PostTricksAsync([Body] CreateTrick createTrick);

        [Get("/api/v1/tricks/{id}")]
        Task<HateoasResult<Trick>> GetTrickByIdAsync(Guid id);

        [Get("/api/v1/stances")]
        Task<HateoasResult<PagedResult<Stance>>> GetStancesAsync([Query] int page = 1, [Query] int pageSize = 10);
    }
}
