using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Simple.Hateoas;
using Simple.Hateoas.Models;
using SkateboardNeverDie.Domain.Skaters.QueryData;

namespace SkateboardNeverDie.Services.Api.HateoasLinkBuilders.Skaters
{
    public class SkaterQueryDataHateoasLinkBuilder : IHateoasLinkBuilder<SkaterQueryData>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;

        public SkaterQueryDataHateoasLinkBuilder(IHttpContextAccessor httpContextAccessor, IAuthorizationService authorizationService)
        {
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
        }

        public HateoasResult<SkaterQueryData> AddLinks(HateoasResult<SkaterQueryData> hateoasResult)
        {
            var authorizationResult = _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, hateoasResult, "Skaters:Remove").Result;

            return hateoasResult
                .AddSelfLink(SkaterRouteNames.GetSkater, _ => new { id = _.Id })
                .AddLink(SkaterRouteNames.GetSkaterTricks, HttpMethod.Get, _ => new { id = _.Id })
                .AddLink(SkaterRouteNames.DeleteSkater, HttpMethod.Delete, _ => new { id = _.Id }, _ => authorizationResult.Succeeded);
        }
    }
}
