using OpenIddict.Abstractions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Net.Security;
using System.Threading.Tasks;

namespace SkateboardNeverDie.Services
{
    public class AuthService
    {
        const string ClientId = "skateboard-mobile";
        const string ClientSecret = "PVHcNBpJX9SmaSk2H+PGHw==";

        readonly string _exchangeUrl;
        readonly HttpClientHandler _httpClientHandler;
        readonly HttpClient _httpClient;

        public AuthService()
        {
            // https://developer.android.com/studio/run/emulator-networking
            _exchangeUrl = App.IsDevelopment ? "https://10.0.2.2:5003/connect/token" : "https://skateboardneverdieservicesauth.azurewebsites.net/connect/token";
            _httpClientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
                {
                    if (App.IsDevelopment)
                        return true;

                    return sslPolicyErrors == SslPolicyErrors.None;
                }
            };
            _httpClient = new HttpClient(_httpClientHandler);
        }

        public async Task<TokenResponse> GetTokenAsync()
        {
            var content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                ["grant_type"] = "client_credentials",
                ["client_id"] = ClientId,
                ["client_secret"] = ClientSecret,
                ["scope"] = "offline_access skateboard-api.read"
            });

            var response = await _httpClient.PostAsync(_exchangeUrl, content);
            var payload = await response.Content.ReadFromJsonAsync<OpenIddictResponse>();

            if (!string.IsNullOrEmpty(payload.Error))
                throw new InvalidOperationException("An error occurred while retrieving an access token.");

            return new TokenResponse(payload.AccessToken, payload.RefreshToken, payload.ExpiresIn.GetValueOrDefault(0));
        }

        public async Task<TokenResponse> RefreshTokenAsync(string refreshToken)
        {
            var content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                ["grant_type"] = "refresh_token",
                ["client_id"] = ClientId,
                ["client_secret"] = ClientSecret,
                ["refresh_token"] = refreshToken
            });

            var response = await _httpClient.PostAsync(_exchangeUrl, content);
            var payload = await response.Content.ReadFromJsonAsync<OpenIddictResponse>();

            if (!string.IsNullOrEmpty(payload.Error))
                throw new InvalidOperationException("An error occurred while retrieving an refresh token.");

            return new TokenResponse(payload.AccessToken, payload.RefreshToken, payload.ExpiresIn.GetValueOrDefault(0));
        }
    }

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
