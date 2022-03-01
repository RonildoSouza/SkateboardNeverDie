using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Simple.Hateoas;
using Simple.Hateoas.Models;
using SkateboardNeverDie.Domain.Tricks.QueryData;

namespace SkateboardNeverDie.Services.Api.HateoasLinkBuilders.Tricks
{
    public class TrickQueryDataHateoasLinkBuilder : IHateoasLinkBuilder<TrickQueryData>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;

        public TrickQueryDataHateoasLinkBuilder(IHttpContextAccessor httpContextAccessor, IAuthorizationService authorizationService)
        {
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
        }

        public HateoasResult<TrickQueryData> Build(HateoasResult<TrickQueryData> hateoasResult)
        {
            var authorizationResult = _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, hateoasResult, "Tricks:Remove").Result;

            return hateoasResult
                .AddSelfLink(TrickRouteNames.GetTrick, _ => new { id = _.Id })
                .AddLink(TrickRouteNames.DeleteTrick, HttpMethod.Delete, _ => new { id = _.Id }, _ => authorizationResult.Succeeded);
        }
    }
}
