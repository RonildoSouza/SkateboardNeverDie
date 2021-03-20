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
                .AddSelfLink(_ => new { page = _.CurrentPage, pageSize = _.PageSize }, StanceRouteNames.GetStances)
                .AddNextLink(_ => new { page = _.CurrentPage + 1, pageSize = _.PageSize }, StanceRouteNames.GetStances, _ => _.CurrentPage < _.PageCount)
                .AddPrevLink(_ => new { page = _.CurrentPage - 1, pageSize = _.PageSize }, StanceRouteNames.GetStances, _ => _.CurrentPage > 1);
        }
    }
}
