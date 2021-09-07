using Microsoft.AspNetCore.Authorization;
using OpenIddict.Abstractions;
using SkateboardNeverDie.Core.Security.Extensions;
using SkateboardNeverDie.Domain.Security.Services;
using System.Threading.Tasks;

namespace SkateboardNeverDie.Services.Api.AuthorizationRequirements
{
    public sealed class PermissionRequirement : IAuthorizationRequirement
    {
        public PermissionRequirement(string permissionId)
        {
            PermissionId = permissionId;
        }

        public string PermissionId { get; }

        public sealed class PermissionHandler : AuthorizationHandler<PermissionRequirement>
        {
            private readonly IUserCacheService _userCacheService;

            public PermissionHandler(IUserCacheService userCacheService)
            {
                _userCacheService = userCacheService;
            }

            protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
            {
                // User is admin or grant type equal client credentials
                if (context.User.HasClaim(OpenIddictConstants.Claims.Role, "admin") || context.User.ValueIsEqualInClaimType("skateboard-api", "sub"))
                {
                    context.Succeed(requirement);
                    return;
                }

                var identityUserId = context.User.GetIdentityUserId();
                var loggedUser = await _userCacheService.GetIdentityUserAuthorizeAsync(identityUserId.GetValueOrDefault());

                if (identityUserId == null || (!loggedUser?.HasPermission(requirement.PermissionId) ?? true))
                    context.Fail();

                context.Succeed(requirement);
            }
        }

        public static class Permissions
        {
            public static class Skaters
            {
                public static PermissionRequirement Add => new("skaters:add");
                public static PermissionRequirement Edit => new("skaters:edit");
                public static PermissionRequirement Remove => new("skaters:remove");
            }

            public static class Tricks
            {
                public static PermissionRequirement Add => new("tricks:add");
                public static PermissionRequirement Edit => new("tricks:edit");
                public static PermissionRequirement Remove => new("tricks:remove");
            }
        }
    }
}
