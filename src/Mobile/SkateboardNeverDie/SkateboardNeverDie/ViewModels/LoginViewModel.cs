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

        UserInfo _userInfo = null;
        public UserInfo UserInfo
        {
            get { return _userInfo; }
            set { SetProperty(ref _userInfo, value); }
        }

        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);
            LogoutCommand = new Command(OnLogoutClicked);
        }

        private async void OnLoginClicked(object obj)
        {
            var tokenResponse = await SingleSignOnService.AuthorizationCodeFlowAsync();

            UserInfo = await SingleSignOnService.UserInfoAsync(tokenResponse.AccessToken);

            await SecureStorageManager.SetAsync(GlobalSetting.TokenResponseKey, tokenResponse);
        }

        private async void OnLogoutClicked(object obj)
        {
            try
            {
                await SingleSignOnService.LogoutAsync("");
                SecureStorage.Remove(GlobalSetting.TokenResponseKey);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }
    }
}
