using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace SkateboardNeverDie.Services
{
    internal class AuthHeaderHandler : HttpClientHandler
    {
        public const string TokenResponseKey = "token_response";

        readonly AuthService _authService;

        public AuthHeaderHandler()
        {
            ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
            {
                if (App.IsDevelopment)
                    return true;

                return sslPolicyErrors == SslPolicyErrors.None;
            };

            _authService = new AuthService();
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var tokenResponse = await GetAsync<TokenResponse>(TokenResponseKey);

            // Get client credentials token
            if (tokenResponse == null || string.IsNullOrEmpty(tokenResponse.AccessToken))
            {
                tokenResponse = await _authService.GetTokenAsync().ConfigureAwait(false);
                await SetAsync(TokenResponseKey, tokenResponse);
            }

            // Execute refresh token
            if (DateTime.UtcNow.AddSeconds(-tokenResponse.ExpiresIn) > DateTime.UtcNow)
            {
                tokenResponse = await _authService.RefreshTokenAsync(tokenResponse.RefreshToken).ConfigureAwait(false);
                await SetAsync(TokenResponseKey, tokenResponse);
            }

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", tokenResponse.AccessToken);

            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }

        async Task SetAsync(string key, object obj)
        {
            var value = JsonConvert.SerializeObject(obj);
            SecureStorage.Remove(key);
            await SecureStorage.SetAsync(key, value);
        }

        async Task<T> GetAsync<T>(string key)
        {
            var value = await SecureStorage.GetAsync(key);

            if (string.IsNullOrEmpty(value))
                return default;

            return JsonConvert.DeserializeObject<T>(value);
        }
    }
}
