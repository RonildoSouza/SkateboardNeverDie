using Refit;
using SkateboardNeverDie.Services;
using System;
using System.Net.Http;
using Xamarin.Forms;

namespace SkateboardNeverDie
{
    public partial class App : Application
    {
        public static bool IsDevelopment = false;

        public App()
        {
#if DEBUG
            IsDevelopment = true;
#endif

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

        void RegisterSkateboardNeverDieApi()
        {
            // https://developer.android.com/studio/run/emulator-networking
            var apiUrl = IsDevelopment ? "https://10.0.2.2:5001" : "https://skateboardneverdieservicesapi.azurewebsites.net";

            var httpClient = new HttpClient(new AuthHeaderHandler())
            {
                BaseAddress = new Uri(apiUrl)
            };

            var skateboardNeverDieApi = RestService.For<ISkateboardNeverDieApi>(httpClient);

            DependencyService.RegisterSingleton(skateboardNeverDieApi);
        }
    }
}
