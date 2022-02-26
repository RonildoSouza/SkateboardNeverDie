using SkateboardNeverDie.Models;
using SkateboardNeverDie.Services;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SkateboardNeverDie.ViewModels
{
    public class DashboardViewModel : BaseViewModel
    {

        private bool _isLogged = false;
        ISingleSignOnService SingleSignOnService => DependencyService.Get<ISingleSignOnService>();
        ISecureStorageManager SecureStorageManager => DependencyService.Get<ISecureStorageManager>();

        public DashboardViewModel()
        {
            Title = "Dashboard";
            RefreshTokenCommand = new Command(OnRefreshTokenClicked);
            CleanTokenCommand = new Command(OnCleanTokenClicked);
            LoginCommand = new Command(OnLoginClicked);
            LogoutCommand = new Command(OnLogoutClicked);
            IsValidTokenAsync().GetAwaiter();
        }

        public Command RefreshTokenCommand { get; }
        public Command CleanTokenCommand { get; }
        public Command LoginCommand { get; }
        public Command LogoutCommand { get; }
        public bool IsLogged
        {
            get => _isLogged;
            set => SetProperty(ref _isLogged, value);
        }


        string _isRefreshed = "false";
        public string IsRefreshed
        {
            get => _isRefreshed;
            set => SetProperty(ref _isRefreshed, value);
        }


        private async void OnRefreshTokenClicked(object obj)
        {
            try
            {
                var tokenResponse = await SecureStorageManager.GetAsync<TokenResponse>(GlobalSetting.TokenResponseKey);
                var tokenResponseTmp = await SingleSignOnService.RefreshTokenFlowAsync(tokenResponse.RefreshToken).ConfigureAwait(false);

                if (tokenResponseTmp != null)
                {
                    await SecureStorageManager.SetAsync(GlobalSetting.TokenResponseKey, tokenResponseTmp);
                    IsRefreshed = $"{tokenResponse.AccessToken == tokenResponseTmp.AccessToken}";
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }

        private void OnCleanTokenClicked(object obj)
        {
            SecureStorageManager.Remove(GlobalSetting.TokenResponseKey);
        }
        private async void OnLoginClicked(object obj)
        {
            var tokenResponse = await SingleSignOnService.AuthorizationCodeFlowAsync();

            if (tokenResponse != null)
            {
                await SecureStorageManager.SetAsync(GlobalSetting.TokenResponseKey, tokenResponse);
                IsLogged = true;
            }
        }

        private async void OnLogoutClicked(object obj)
        {
            try
            {
                var tokenResponse = await SecureStorageManager.GetAsync<TokenResponse>(GlobalSetting.TokenResponseKey);

                if (await SingleSignOnService.LogoutAsync(tokenResponse.IdentityToken))
                {
                    SecureStorageManager.Remove(GlobalSetting.TokenResponseKey);
                    IsLogged = false;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }

        private async Task IsValidTokenAsync()
        {
            var tokenResponse = await SecureStorageManager.GetAsync<TokenResponse>(GlobalSetting.TokenResponseKey);
            IsLogged = tokenResponse != null && !tokenResponse.IsExpired && !string.IsNullOrEmpty(tokenResponse.IdentityToken);
        }
    }
}
