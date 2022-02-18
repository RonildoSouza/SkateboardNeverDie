using Simple.Hateoas;
using Simple.Hateoas.Models;
using SkateboardNeverDie.Core.Domain;
using SkateboardNeverDie.Domain.Skaters.QueryData;

namespace SkateboardNeverDie.Services.Api.HateoasLinkBuilders.Skaters
{
    public class PagedResultSkaterTrickQueryDataHateoasLinkBuilder : IHateoasLinkBuilder<PagedResult<SkaterTrickQueryData>>
    {
        public HateoasResult<PagedResult<SkaterTrickQueryData>> Build(HateoasResult<PagedResult<SkaterTrickQueryData>> hateoasResult)
        {
            var skaterId = hateoasResult.GetArg(0);

            return hateoasResult
                .AddSelfLink(SkaterRouteNames.GetSkaterTricks, _ => new { id = skaterId, page = _.CurrentPage, pageSize = _.PageSize })
                .AddNextLink(SkaterRouteNames.GetSkaterTricks, _ => new { id = skaterId, page = _.CurrentPage + 1, pageSize = _.PageSize }, _ => _.CurrentPage < _.PageCount)
                .AddPrevLink(SkaterRouteNames.GetSkaterTricks, _ => new { id = skaterId, page = _.CurrentPage - 1, pageSize = _.PageSize }, _ => _.CurrentPage > 1);
        }
    }
}
