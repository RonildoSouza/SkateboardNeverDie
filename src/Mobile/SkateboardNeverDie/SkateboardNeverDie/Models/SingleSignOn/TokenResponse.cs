namespace SkateboardNeverDie.Models
{
    public sealed class TokenResponse
    {
        public TokenResponse(string accessToken, string refreshToken, long expiresIn, string identityToken = default)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
            ExpiresIn = expiresIn;
            IdentityToken = identityToken;
        }

        public string AccessToken { get; }
        public string RefreshToken { get; }
        public long ExpiresIn { get; }
        public string IdentityToken { get; }
    }
}
