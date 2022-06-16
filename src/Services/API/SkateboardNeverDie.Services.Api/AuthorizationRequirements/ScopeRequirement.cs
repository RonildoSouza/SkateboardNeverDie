using Microsoft.AspNetCore.Authorization;
using OpenIddict.Abstractions;
using System.Threading.Tasks;

namespace SkateboardNeverDie.Services.Api.AuthorizationRequirements
{
    public sealed class ScopeRequirement : IAuthorizationRequirement
    {
        public ScopeRequirement(string scope)
        {
            Scope = scope;
        }

        public string Scope { get; }

        public sealed class ScopeHandler : AuthorizationHandler<ScopeRequirement>
        {
            protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ScopeRequirement requirement)
            {
                if (!context.User.HasScope(requirement.Scope))
                {
                    context.Fail();
                    return Task.CompletedTask;
                }

                context.Succeed(requirement);

                return Task.CompletedTask;
            }
        }

        public static class Scopes
        {
            public static ScopeRequirement SkateboardApiRead => new("skateboard-api:read");
        }
    }
}
