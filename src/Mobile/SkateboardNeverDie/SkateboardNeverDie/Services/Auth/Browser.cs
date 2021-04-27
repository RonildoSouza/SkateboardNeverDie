using IdentityModel.OidcClient.Browser;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace SkateboardNeverDie.Services
{
    public sealed class Browser : IBrowser
    {
        public async Task<BrowserResult> InvokeAsync(BrowserOptions options, CancellationToken cancellationToken = default)
        {
            var authResult = await WebAuthenticator.AuthenticateAsync(new Uri(options.StartUrl), new Uri(GlobalSetting.RedirectUri));

            return new BrowserResult()
            {
                Response = ParseAuthenticatorResult(authResult)
            };
        }

        string ParseAuthenticatorResult(WebAuthenticatorResult result)
        {
            string code = result?.Properties["code"];
            string state = result?.Properties["state"];

            return $"{GlobalSetting.RedirectUri}#code={code}&state={state}";
        }
    }
}
