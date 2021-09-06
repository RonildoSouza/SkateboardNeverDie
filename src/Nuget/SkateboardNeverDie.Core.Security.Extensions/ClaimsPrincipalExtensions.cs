using System;
using System.Security.Claims;

namespace SkateboardNeverDie.Core.Security.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid? GetIdentityUserId(this ClaimsPrincipal ClaimsPrincipal, string claimType = "sub")
        {
            if (!ClaimsPrincipal.HasClaim(_ => _.Type == claimType))
                return null;

            return Guid.Parse(ClaimsPrincipal.FindFirst(claimType).Value);
        }
    }
}
