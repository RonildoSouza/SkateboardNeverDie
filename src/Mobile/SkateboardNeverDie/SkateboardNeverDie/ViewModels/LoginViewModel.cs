using SkateboardNeverDie.Services;
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

        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);
            LogoutCommand = new Command(OnLogoutClicked);
        }

        private async void OnLoginClicked(object obj)
        {
            var tokenResponse = await SingleSignOnService.AuthorizationCodeFlowAsync();
            await SecureStorageManager.SetAsync(GlobalSetting.TokenResponseKey, tokenResponse);
        }

        private async void OnLogoutClicked(object obj)
        {
            //await SingleSignOnService.LogoutAsync();
            //SecureStorage.Remove(GlobalSetting.TokenResponseKey);
        }
    }
}
