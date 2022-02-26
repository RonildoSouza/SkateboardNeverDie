using IdentityModel.OidcClient;
using OpenIddict.Abstractions;
using SkateboardNeverDie.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Net.Security;
using System.Threading.Tasks;

namespace SkateboardNeverDie.Services
{
    internal sealed class SingleSignOnService : ISingleSignOnService
    {
        private readonly HttpClientHandler _httpClientHandler;
        private readonly HttpClient _httpClient;

        public SingleSignOnService()
        {
            _httpClientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
                {
                    if (GlobalSetting.IsDevelopment())
                        return true;

                    return sslPolicyErrors == SslPolicyErrors.None;
                }
            };

            _httpClient = new HttpClient(_httpClientHandler);
        }

        private OidcClient OidcClient
            => new OidcClient(new OidcClientOptions
            {
                Authority = GlobalSetting.SsoUrl,
                ClientId = GlobalSetting.ClientId,
                ClientSecret = GlobalSetting.ClientSecret,
                Scope = GlobalSetting.ScopeAuthorizationCode,
                RedirectUri = GlobalSetting.RedirectUri,
                PostLogoutRedirectUri = GlobalSetting.RedirectUri,
                Browser = new WebAuthenticatorBrowser()
            });

        public async Task<TokenResponse> ClientCredentialsFlowAsync()
        {
            var content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                ["grant_type"] = "client_credentials",
                ["client_id"] = GlobalSetting.ClientId,
                ["client_secret"] = GlobalSetting.ClientSecret,
                ["scope"] = GlobalSetting.ScopeClientCredentials
            });

            var response = await _httpClient.PostAsync($"{GlobalSetting.SsoUrl}/connect/token", content);
            var payload = await response.Content.ReadFromJsonAsync<OpenIddictResponse>();

            return !string.IsNullOrEmpty(payload.Error)
                ? throw new InvalidOperationException("An error occurred while retrieving an access token.")
                : new TokenResponse(payload.AccessToken, payload.RefreshToken, payload.ExpiresIn.GetValueOrDefault(0), DateTimeOffset.UtcNow);
        }

        public async Task<TokenResponse> RefreshTokenFlowAsync(string refreshToken)
        {
            var refreshTokenResult = await OidcClient.RefreshTokenAsync(refreshToken);

            return !string.IsNullOrEmpty(refreshTokenResult.Error)
                ? null
                : new TokenResponse(refreshTokenResult.AccessToken, refreshTokenResult.RefreshToken, refreshTokenResult.ExpiresIn, DateTimeOffset.UtcNow, refreshTokenResult.IdentityToken);
        }

        public async Task<TokenResponse> AuthorizationCodeFlowAsync()
        {
            try
            {
                var loginResult = await OidcClient.LoginAsync();

                return loginResult.IsError
                    ? throw new InvalidOperationException(loginResult.Error)
                    : new TokenResponse(loginResult.AccessToken, loginResult.RefreshToken, loginResult.TokenResponse.ExpiresIn, DateTimeOffset.UtcNow, loginResult.IdentityToken);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("System.Threading.Tasks.TaskCanceledException"))
                    return null;

                throw;
            }
        }

        public async Task<bool> LogoutAsync(string identityToken)
        {
            var logoutResult = await OidcClient.LogoutAsync(new LogoutRequest
            {
                IdTokenHint = identityToken
            });

            return !logoutResult.IsError;
        }
    }
}
