using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Simple.Hateoas;
using Simple.Hateoas.Models;
using SkateboardNeverDie.Core.Domain;
using SkateboardNeverDie.Domain.Tricks.QueryData;

namespace SkateboardNeverDie.Services.Api.HateoasLinkBuilders.Tricks
{
    public class PagedResultTrickQueryDataHateoasLinkBuilder : IHateoasLinkBuilder<PagedResult<TrickQueryData>>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;

        public PagedResultTrickQueryDataHateoasLinkBuilder(IHttpContextAccessor httpContextAccessor, IAuthorizationService authorizationService)
        {
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
        }

        public HateoasResult<PagedResult<TrickQueryData>> AddLinks(HateoasResult<PagedResult<TrickQueryData>> hateoasResult)
        {
            var authorizationResult = _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, hateoasResult, "Tricks:Add").Result;

            return hateoasResult
                .AddSelfLink(TrickRouteNames.GetTricks, _ => new { page = _.CurrentPage, pageSize = _.PageSize })
                .AddNextLink(TrickRouteNames.GetTricks, _ => new { page = _.CurrentPage + 1, pageSize = _.PageSize }, _ => _.CurrentPage < _.PageCount)
                .AddPrevLink(TrickRouteNames.GetTricks, _ => new { page = _.CurrentPage - 1, pageSize = _.PageSize }, _ => _.CurrentPage > 1)
                .AddLink(TrickRouteNames.CreateTrick, HttpMethod.Post, _ => authorizationResult.Succeeded);
        }
    }
}
