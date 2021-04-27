using SkateboardNeverDie.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SkateboardNeverDie.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        IAuthService AuthService => DependencyService.Get<IAuthService>();
        ISecureStorageManager SecureStorageManager => DependencyService.Get<ISecureStorageManager>();

        public Command LoginCommand { get; }
        public Command LogoutCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);
            LogoutCommand = new Command(OnLogoutClicked);
        }

        private async void OnLoginClicked(object obj)
        {
            var tokenResponse = await AuthService.AuthorizationCodeFlowAsync();
            await SecureStorageManager.SetAsync(GlobalSetting.TokenResponseKey, tokenResponse);
        }

        private async void OnLogoutClicked(object obj)
        {
            //await AuthService.LogoutAsync();
            //SecureStorage.Remove(GlobalSetting.TokenResponseKey);
        }
    }
}
