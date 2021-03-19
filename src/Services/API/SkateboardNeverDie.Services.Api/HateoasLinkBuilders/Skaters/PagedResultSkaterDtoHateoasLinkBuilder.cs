using Simple.Hateoas;
using Simple.Hateoas.Models;
using SkateboardNeverDie.Application.Skaters.Dtos;
using SkateboardNeverDie.Core.Application;

namespace SkateboardNeverDie.Services.Api.HateoasLinkBuilders.Skaters
{
    public class PagedResultSkaterDtoHateoasLinkBuilder : IHateoasLinkBuilder<PagedResult<SkaterDto>>
    {
        public HateoasResult<PagedResult<SkaterDto>> Build(HateoasResult<PagedResult<SkaterDto>> hateoasResult)
        {
            return hateoasResult
                .AddLink(SkaterRouteNames.CreateSkater, HttpMethod.Post)
                .AddNextLink(_ => new { page = _.CurrentPage + 1, pageSize = _.PageSize }, SkaterRouteNames.GetSkaters, _ => _.CurrentPage < _.PageCount)
                .AddPrevLink(_ => new { page = _.CurrentPage - 1, pageSize = _.PageSize }, SkaterRouteNames.GetSkaters, _ => _.CurrentPage > 1);
        }
    }
}
