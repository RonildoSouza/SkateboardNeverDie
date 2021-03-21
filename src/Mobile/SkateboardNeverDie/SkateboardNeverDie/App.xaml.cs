using Refit;
using SkateboardNeverDie.Services;
using System;
using System.Net.Http;
using System.Net.Security;
using Xamarin.Forms;

namespace SkateboardNeverDie
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            RegisterSkateboardNeverDieApi();

            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        private void RegisterSkateboardNeverDieApi()
        {
            var development = false;

#if DEBUG
            development = true;
#endif

            // https://developer.android.com/studio/run/emulator-networking
            var apiUrl = development ? "https://10.0.2.2:5001" : string.Empty;
            var httpClientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
                {
                    if (development)
                        return true;

                    return sslPolicyErrors == SslPolicyErrors.None;
                }
            };

            var httpClient = new HttpClient(httpClientHandler)
            {
                BaseAddress = new Uri(apiUrl)
            };

            var skateboardNeverDieApi = RestService.For<ISkateboardNeverDieApi>(httpClient);

            DependencyService.RegisterSingleton(skateboardNeverDieApi);
        }
    }
}
