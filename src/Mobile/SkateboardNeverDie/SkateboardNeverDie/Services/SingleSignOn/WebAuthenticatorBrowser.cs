using IdentityModel.OidcClient.Browser;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace SkateboardNeverDie.Services
{
    public sealed class WebAuthenticatorBrowser : IBrowser
    {
        public async Task<BrowserResult> InvokeAsync(BrowserOptions options, CancellationToken cancellationToken = default)
        {
            try
            {
                var webAuthenticatorResult = await WebAuthenticator.AuthenticateAsync(new Uri(options.StartUrl), new Uri(options.EndUrl));

                return new BrowserResult()
                {
                    Response = ParseAuthenticatorResult(webAuthenticatorResult, options.EndUrl)
                };
            }
            catch (Exception ex)
            {
                return new BrowserResult()
                {
                    ResultType = BrowserResultType.UnknownError,
                    Error = ex.ToString()
                };
            }
        }

        string ParseAuthenticatorResult(WebAuthenticatorResult webAuthenticatorResult, string callbackUrl)
        {
            var parameters = webAuthenticatorResult.Properties.Select(p => $"{p.Key}={p.Value}");
            var values = string.Join("&", parameters);

            return $"{callbackUrl}#{values}";
        }
    }
}
