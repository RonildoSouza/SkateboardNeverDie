using Simple.Hateoas;
using Simple.Hateoas.Models;
using SkateboardNeverDie.Core.Domain;
using SkateboardNeverDie.Domain.Tricks.QueryData;

namespace SkateboardNeverDie.Services.Api.HateoasLinkBuilders.Tricks
{
    public class PagedResultTrickQueryDataHateoasLinkBuilder : IHateoasLinkBuilder<PagedResult<TrickQueryData>>
    {
        public HateoasResult<PagedResult<TrickQueryData>> Build(HateoasResult<PagedResult<TrickQueryData>> hateoasResult)
        {
            return hateoasResult
                .AddSelfLink(_ => new { page = _.CurrentPage, pageSize = _.PageSize }, TrickRouteNames.GetTricks)
                .AddNextLink(_ => new { page = _.CurrentPage + 1, pageSize = _.PageSize }, TrickRouteNames.GetTricks, _ => _.CurrentPage < _.PageCount)
                .AddPrevLink(_ => new { page = _.CurrentPage - 1, pageSize = _.PageSize }, TrickRouteNames.GetTricks, _ => _.CurrentPage > 1)
                .AddLink(TrickRouteNames.CreateTrick, HttpMethod.Post);
        }
    }
}
