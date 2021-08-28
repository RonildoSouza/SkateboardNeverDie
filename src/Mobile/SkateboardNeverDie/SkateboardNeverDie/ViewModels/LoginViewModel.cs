using SkateboardNeverDie.Models;
using SkateboardNeverDie.Services;
using System;
using System.Diagnostics;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SkateboardNeverDie.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        ISingleSignOnService SingleSignOnService => DependencyService.Get<ISingleSignOnService>();
        ISecureStorageManager SecureStorageManager => DependencyService.Get<ISecureStorageManager>();

        public Command LoginCommand { get; }
        public Command LogoutCommand { get; }

        string _accessToken = null;
        public string AccessToken
        {
            get => _accessToken;
            set => SetProperty(ref _accessToken, value);
        }

        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);
            LogoutCommand = new Command(OnLogoutClicked);
        }

        private async void OnLoginClicked(object obj)
        {
            var tokenResponse = await SingleSignOnService.AuthorizationCodeFlowAsync();

            if (tokenResponse != null)
            {
                AccessToken = tokenResponse.AccessToken;
                await SecureStorageManager.SetAsync(GlobalSetting.TokenResponseKey, tokenResponse);
            }
            else
            {
                AccessToken = "CLOSED THE LOGIN PAGE!";
            }
        }

        private async void OnLogoutClicked(object obj)
        {
            try
            {
                var tokenResponse = await SecureStorageManager.GetAsync<TokenResponse>(GlobalSetting.TokenResponseKey);

                if (await SingleSignOnService.LogoutAsync(tokenResponse.IdentityToken) && SecureStorage.Remove(GlobalSetting.TokenResponseKey))
                {
                    AccessToken = null;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }
    }
}
