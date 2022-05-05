using System;

namespace SkateboardNeverDie.Models
{
    public sealed class TokenResponse
    {
        public TokenResponse(string accessToken, string refreshToken, long expiresIn, DateTimeOffset issuedAt, string identityToken = default)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
            ExpiresIn = expiresIn;
            IdentityToken = identityToken;
            IssuedAt = issuedAt;
        }

        public string AccessToken { get; }
        public string RefreshToken { get; }
        public long ExpiresIn { get; }
        public string IdentityToken { get; }
        public DateTimeOffset IssuedAt { get; }
        public bool IsExpired => DateTimeOffset.UtcNow.AddSeconds(-(ExpiresIn * 1.1f)) >= IssuedAt;
    }
}
