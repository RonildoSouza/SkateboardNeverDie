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
        readonly HttpClientHandler _httpClientHandler;
        readonly HttpClient _httpClient;

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

            if (!string.IsNullOrEmpty(payload.Error))
                throw new InvalidOperationException("An error occurred while retrieving an access token.");

            return new TokenResponse(payload.AccessToken, payload.RefreshToken, payload.ExpiresIn.GetValueOrDefault(0));
        }

        public async Task<TokenResponse> RefreshTokenFlowAsync(string refreshToken)
        {
            var content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                ["grant_type"] = "refresh_token",
                ["client_id"] = GlobalSetting.ClientId,
                ["client_secret"] = GlobalSetting.ClientSecret,
                ["refresh_token"] = refreshToken
            });

            var response = await _httpClient.PostAsync(GlobalSetting.SsoUrl, content);
            var payload = await response.Content.ReadFromJsonAsync<OpenIddictResponse>();

            if (!string.IsNullOrEmpty(payload.Error))
                throw new InvalidOperationException("An error occurred while retrieving an refresh token.");

            return new TokenResponse(payload.AccessToken, payload.RefreshToken, payload.ExpiresIn.GetValueOrDefault(0));
        }

        public async Task<TokenResponse> AuthorizationCodeFlowAsync()
        {
            var oidcClientOptions = new OidcClientOptions
            {
                Authority = GlobalSetting.SsoUrl,
                ClientId = GlobalSetting.ClientId,
                ClientSecret = GlobalSetting.ClientSecret,
                Scope = GlobalSetting.ScopeAuthorizationCode,
                RedirectUri = GlobalSetting.RedirectUri,
                Browser = new Browser()
            };

            var oidcClient = new OidcClient(oidcClientOptions);

            var loginResult = await oidcClient.LoginAsync();

            if (loginResult.IsError)
                throw new InvalidOperationException("An error occurred while retrieving an access token.");

            return new TokenResponse(loginResult.AccessToken, loginResult.RefreshToken, loginResult.TokenResponse.ExpiresIn);
        }

        public async Task LogoutAsync()
        {
            throw new NotImplementedException();
        }
    }
}
