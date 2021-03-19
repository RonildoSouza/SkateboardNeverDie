using Simple.Hateoas;
using Simple.Hateoas.Models;
using SkateboardNeverDie.Application.Stances.Dtos;
using SkateboardNeverDie.Core.Application;

namespace SkateboardNeverDie.Services.Api.HateoasLinkBuilders.Stances
{
    public class PagedResultStanceDtoHateoasLinkBuilder : IHateoasLinkBuilder<PagedResult<StanceDto>>
    {
        public HateoasResult<PagedResult<StanceDto>> Build(HateoasResult<PagedResult<StanceDto>> hateoasResult)
        {
            return hateoasResult
                .AddNextLink(_ => new { page = _.CurrentPage + 1, pageSize = _.PageSize }, StanceRouteNames.GetStances, _ => _.CurrentPage < _.PageCount)
                .AddPrevLink(_ => new { page = _.CurrentPage - 1, pageSize = _.PageSize }, StanceRouteNames.GetStances, _ => _.CurrentPage > 1);
        }
    }
}
