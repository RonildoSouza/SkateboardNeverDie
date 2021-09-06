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

        public HateoasResult<PagedResult<SkaterQueryData>> Build(HateoasResult<PagedResult<SkaterQueryData>> hateoasResult)
        {
            var authorizationResult = _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, hateoasResult, "Skaters:Add").Result;

            return hateoasResult
                .AddSelfLink(_ => new { page = _.CurrentPage, pageSize = _.PageSize }, SkaterRouteNames.GetSkaters)
                .AddNextLink(_ => new { page = _.CurrentPage + 1, pageSize = _.PageSize }, SkaterRouteNames.GetSkaters, _ => _.CurrentPage < _.PageCount)
                .AddPrevLink(_ => new { page = _.CurrentPage - 1, pageSize = _.PageSize }, SkaterRouteNames.GetSkaters, _ => _.CurrentPage > 1)
                .AddLink(SkaterRouteNames.CreateSkater, HttpMethod.Post, _ => authorizationResult.Succeeded);
        }
    }
}
