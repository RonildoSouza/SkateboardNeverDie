using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Simple.Hateoas;
using Simple.Hateoas.Models;
using SkateboardNeverDie.Core.Domain;
using SkateboardNeverDie.Domain.Skaters.QueryData;

namespace SkateboardNeverDie.Services.Api.HateoasLinkBuilders.Skaters
{
    public class PagedResultSkaterQueryDataHateoasLinkBuilder : IHateoasLinkBuilder<PagedResult<SkaterQueryData>>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;

        public PagedResultSkaterQueryDataHateoasLinkBuilder(IHttpContextAccessor httpContextAccessor, IAuthorizationService authorizationService)
        {
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
        }

        public HateoasResult<PagedResult<SkaterQueryData>> AddLinks(HateoasResult<PagedResult<SkaterQueryData>> hateoasResult)
        {
            var authorizationResult = _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, hateoasResult, "Skaters:Add").Result;

            return hateoasResult
                .AddSelfLink(SkaterRouteNames.GetSkaters, _ => new { page = _.CurrentPage, pageSize = _.PageSize })
                .AddNextLink(SkaterRouteNames.GetSkaters, _ => new { page = _.CurrentPage + 1, pageSize = _.PageSize }, _ => _.CurrentPage < _.PageCount)
                .AddPrevLink(SkaterRouteNames.GetSkaters, _ => new { page = _.CurrentPage - 1, pageSize = _.PageSize }, _ => _.CurrentPage > 1)
                .AddLink(SkaterRouteNames.CreateSkater, HttpMethod.Post, _ => authorizationResult.Succeeded);
        }
    }
}
