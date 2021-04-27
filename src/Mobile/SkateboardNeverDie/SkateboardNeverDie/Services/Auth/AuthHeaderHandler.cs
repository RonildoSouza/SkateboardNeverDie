using SkateboardNeverDie.Models;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SkateboardNeverDie.Services
{
    internal class AuthHeaderHandler : HttpClientHandler
    {
        readonly AuthService _authService;

        ISecureStorageManager SecureStorageManager => DependencyService.Get<ISecureStorageManager>();

        public AuthHeaderHandler()
        {
            ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
            {
                if (GlobalSetting.IsDevelopment())
                    return true;

                return sslPolicyErrors == SslPolicyErrors.None;
            };

            _authService = new AuthService();
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var tokenResponse = await SecureStorageManager.GetAsync<TokenResponse>(GlobalSetting.TokenResponseKey);

            // Get client credentials token
            if (tokenResponse == null || string.IsNullOrEmpty(tokenResponse.AccessToken))
            {
                tokenResponse = await _authService.ClientCredentialsFlowAsync().ConfigureAwait(false);
                await SecureStorageManager.SetAsync(GlobalSetting.TokenResponseKey, tokenResponse);
            }

            // Execute refresh token
            if (DateTimeOffset.UtcNow.AddSeconds(-tokenResponse.ExpiresIn) > DateTimeOffset.UtcNow)
            {
                tokenResponse = await _authService.RefreshTokenFlowAsync(tokenResponse.RefreshToken).ConfigureAwait(false);
                await SecureStorageManager.SetAsync(GlobalSetting.TokenResponseKey, tokenResponse);
            }

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", tokenResponse.AccessToken);

            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }
    }
}
