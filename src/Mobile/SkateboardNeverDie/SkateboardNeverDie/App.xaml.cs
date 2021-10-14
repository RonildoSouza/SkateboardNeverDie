using Refit;
using SkateboardNeverDie.Services;
using System;
using System.Net.Http;
using Xamarin.Forms;

namespace SkateboardNeverDie
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            RegisterSkateboardNeverDieApi();

            DependencyService.RegisterSingleton<ISecureStorageManager>(new SecureStorageManager());
            DependencyService.Register<ISingleSignOnService, SingleSignOnService>();

            MainPage = new AppShell();
        }

        protected override void OnStart() { }

        protected override void OnSleep() { }

        protected override void OnResume() { }

        private void RegisterSkateboardNeverDieApi()
        {
            var httpClient = new HttpClient(new AuthorizationHeaderHandler())
            {
                BaseAddress = new Uri(GlobalSetting.ApiUrl)
            };

            var skateboardNeverDieApi = RestService.For<ISkateboardNeverDieApi>(httpClient);

            DependencyService.RegisterSingleton(skateboardNeverDieApi);
        }
    }
}
