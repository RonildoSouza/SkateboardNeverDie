namespace SkateboardNeverDie.Models
{
    public sealed class TokenResponse
    {
        public TokenResponse(string accessToken, string refreshToken, long expiresIn)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
            ExpiresIn = expiresIn;
        }

        public string AccessToken { get; }
        public string RefreshToken { get; }
        public long ExpiresIn { get; }
    }
}
