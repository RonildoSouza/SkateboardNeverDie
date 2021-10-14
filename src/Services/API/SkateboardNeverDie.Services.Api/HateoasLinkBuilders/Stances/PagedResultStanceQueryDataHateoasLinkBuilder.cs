using Simple.Hateoas;
using Simple.Hateoas.Models;
using SkateboardNeverDie.Core.Domain;
using SkateboardNeverDie.Domain.Stances.QueryData;

namespace SkateboardNeverDie.Services.Api.HateoasLinkBuilders.Stances
{
    public class PagedResultStanceQueryDataHateoasLinkBuilder : IHateoasLinkBuilder<PagedResult<StanceQueryData>>
    {
        public HateoasResult<PagedResult<StanceQueryData>> Build(HateoasResult<PagedResult<StanceQueryData>> hateoasResult)
        {
            return hateoasResult
                .AddSelfLink(StanceRouteNames.GetStances, _ => new { page = _.CurrentPage, pageSize = _.PageSize })
                .AddNextLink(StanceRouteNames.GetStances, _ => new { page = _.CurrentPage + 1, pageSize = _.PageSize }, _ => _.CurrentPage < _.PageCount)
                .AddPrevLink(StanceRouteNames.GetStances, _ => new { page = _.CurrentPage - 1, pageSize = _.PageSize }, _ => _.CurrentPage > 1);
        }
    }
}
