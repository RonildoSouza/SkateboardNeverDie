using Simple.Hateoas;
using Simple.Hateoas.Models;
using SkateboardNeverDie.Core.Domain;
using SkateboardNeverDie.Domain.QueryData;

namespace SkateboardNeverDie.Services.Api.HateoasLinkBuilders.Skaters
{
    public class PagedResultSkaterQueryDataHateoasLinkBuilder : IHateoasLinkBuilder<PagedResult<SkaterQueryData>>
    {
        public HateoasResult<PagedResult<SkaterQueryData>> Build(HateoasResult<PagedResult<SkaterQueryData>> hateoasResult)
        {
            return hateoasResult
                .AddSelfLink(_ => new { page = _.CurrentPage, pageSize = _.PageSize }, SkaterRouteNames.GetSkaters)
                .AddNextLink(_ => new { page = _.CurrentPage + 1, pageSize = _.PageSize }, SkaterRouteNames.GetSkaters, _ => _.CurrentPage < _.PageCount)
                .AddPrevLink(_ => new { page = _.CurrentPage - 1, pageSize = _.PageSize }, SkaterRouteNames.GetSkaters, _ => _.CurrentPage > 1)
                .AddLink(SkaterRouteNames.CreateSkater, HttpMethod.Post);
        }
    }
}
