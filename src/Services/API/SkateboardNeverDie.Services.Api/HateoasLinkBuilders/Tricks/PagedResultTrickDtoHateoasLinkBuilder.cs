using Simple.Hateoas;
using Simple.Hateoas.Models;
using SkateboardNeverDie.Application.Tricks.Dtos;
using SkateboardNeverDie.Core.Application;

namespace SkateboardNeverDie.Services.Api.HateoasLinkBuilders.Tricks
{
    public class PagedResultTrickDtoHateoasLinkBuilder : IHateoasLinkBuilder<PagedResult<TrickDto>>
    {
        public HateoasResult<PagedResult<TrickDto>> Build(HateoasResult<PagedResult<TrickDto>> hateoasResult)
        {
            return hateoasResult
                .AddNextLink(_ => new { page = _.CurrentPage + 1, pageSize = _.PageSize }, TrickRouteNames.GetTricks, _ => _.CurrentPage < _.PageCount)
                .AddPrevLink(_ => new { page = _.CurrentPage - 1, pageSize = _.PageSize }, TrickRouteNames.GetTricks, _ => _.CurrentPage > 1);
        }
    }
}
