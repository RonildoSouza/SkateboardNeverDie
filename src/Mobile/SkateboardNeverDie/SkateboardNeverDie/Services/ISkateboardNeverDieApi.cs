using Refit;
using SkateboardNeverDie.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SkateboardNeverDie.Services
{
    public interface ISkateboardNeverDieApi
    {
        #region Skaters
        [Get("/api/v1/skaters")]
        Task<HateoasResult<PagedResult<Skater>>> GetSkatersAsync([Query] int page = 1, [Query] int pageSize = 10);

        [Post("/api/v1/skaters")]
        Task<HateoasResult<Skater>> PostSkatersAsync([Body] CreateSkater createSkater);

        [Get("/api/v1/skaters/{id}")]
        Task<HateoasResult<Skater>> GetSkaterByIdAsync(Guid id);

        [Get("/api/v1/skaters/{id}/tricks")]
        Task<HateoasResult<PagedResult<SkaterTrick>>> GetSkaterTricksAsync(Guid id, [Query] int page = 1, [Query] int pageSize = 10);

        [Delete("/api/v1/skaters/{id}")]
        Task DeleteSkaterAsync(Guid id);
        #endregion

        #region Tricks
        [Get("/api/v1/tricks")]
        Task<HateoasResult<PagedResult<Trick>>> GetTricksAsync([Query] int page = 1, [Query] int pageSize = 10);

        [Post("/api/v1/tricks")]
        Task<HateoasResult<Trick>> PostTricksAsync([Body] CreateTrick createTrick);

        [Get("/api/v1/tricks/{id}")]
        Task<HateoasResult<Trick>> GetTrickByIdAsync(Guid id);

        [Delete("/api/v1/tricks/{id}")]
        Task DeleteTrickAsync(Guid id);
        #endregion

        #region Stances
        [Get("/api/v1/stances")]
        Task<HateoasResult<PagedResult<Stance>>> GetStancesAsync([Query] int page = 1, [Query] int pageSize = 10);
        #endregion

        #region Dashboard
        [Get("/api/v1/dashboard/tricks/count")]
        Task<int> GetTricksCountAsync();

        [Get("/api/v1/dashboard/skaters/count")]
        Task<int> GetSkatersCountAsync();

        [Get("/api/v1/dashboard/skaters/goofy-vs-regular")]
        Task<IDictionary<StanceType, int>> GetSkatersGoofyVsRegularAsync();

        [Get("/api/v1/dashboard/skaters/count-per-age")]
        Task<IList<SkaterCountPerAge>> GetSkatersCountPerAgeAsync();
        #endregion
    }
}
