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

            Guid.TryParse(ClaimsPrincipal.FindFirst(claimType).Value, out Guid identityUserId);
            return identityUserId;
        }

        public static bool ValueIsEqualInClaimType(this ClaimsPrincipal ClaimsPrincipal, string value, string claimType)
        {
            if (!ClaimsPrincipal.HasClaim(_ => _.Type == claimType))
                return false;

            return ClaimsPrincipal.FindFirst(claimType).Value == value;
        }
    }
}
