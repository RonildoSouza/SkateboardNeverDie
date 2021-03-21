using Refit;
using SkateboardNeverDie.Models;
using System;
using System.Collections.Generic;
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

    public sealed class HateoasResult<T>
    {
        public T Data { get; set; }
        public IEnumerable<HateoasLink> Links { get; set; }

        public sealed class HateoasLink
        {
            public string Href { get; set; }
            public string Rel { get; set; }
            public string Method { get; set; }
        }
    }
}
