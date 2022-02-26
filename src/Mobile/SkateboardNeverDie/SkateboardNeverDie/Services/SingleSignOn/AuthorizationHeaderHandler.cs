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
    internal class AuthorizationHeaderHandler : HttpClientHandler
    {
        ISingleSignOnService SingleSignOnService => DependencyService.Get<ISingleSignOnService>();
        ISecureStorageManager SecureStorageManager => DependencyService.Get<ISecureStorageManager>();

        public AuthorizationHeaderHandler()
        {
            ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
            {
                if (GlobalSetting.IsDevelopment())
                    return true;

                return sslPolicyErrors == SslPolicyErrors.None;
            };
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var tokenResponse = await SecureStorageManager.GetAsync<TokenResponse>(GlobalSetting.TokenResponseKey);

            // Execute refresh token
            if (tokenResponse != null && tokenResponse.IsExpired && tokenResponse.IssuedAt.Date == DateTimeOffset.UtcNow.Date)
            {
                tokenResponse = await SingleSignOnService.RefreshTokenFlowAsync(tokenResponse.RefreshToken).ConfigureAwait(false);
                await SecureStorageManager.SetAsync(GlobalSetting.TokenResponseKey, tokenResponse);
            }
            
            // Get client credentials token
            if(tokenResponse == null || string.IsNullOrEmpty(tokenResponse?.AccessToken) || tokenResponse.IssuedAt.AddDays(1).Date == DateTimeOffset.UtcNow.Date || tokenResponse.IsExpired)
            {
                tokenResponse = await SingleSignOnService.ClientCredentialsFlowAsync().ConfigureAwait(false);
                await SecureStorageManager.SetAsync(GlobalSetting.TokenResponseKey, tokenResponse);
            }

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", tokenResponse.AccessToken);

            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }
    }
}
