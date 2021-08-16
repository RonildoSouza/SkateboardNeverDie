using static IdentityModel.OidcConstants;

namespace SkateboardNeverDie
{
    internal static class GlobalSetting
    {
        internal const string TokenResponseKey = "token_response";

        internal const string ClientId = "skateboard-mobile";
        internal const string ClientSecret = "PVHcNBpJX9SmaSk2H+PGHw==";
        internal const string RedirectUri = "myapp://";

        // https://developer.android.com/studio/run/emulator-networking
        internal const string SsoUrl = "https://skateboardneverdieservicessinglesignon.azurewebsites.net";
        internal static string ApiUrl => IsDevelopment() ? "https://10.0.2.2:5001" : "https://skateboardneverdieservicesapi.azurewebsites.net";
        internal static string ScopeClientCredentials => $"{StandardScopes.OfflineAccess} skateboard-api:read";
        internal static string ScopeAuthorizationCode => $"{ScopeClientCredentials} {StandardScopes.OpenId} {StandardScopes.Profile} {StandardScopes.Email} roles";

        internal static bool IsDevelopment()
        {
            var isDevelopment = false;

#if !DEBUG
            isDevelopment = true;
#endif

            return isDevelopment;
        }
    }
}
