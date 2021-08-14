using SkateboardNeverDie.Models;
using System.Threading.Tasks;

namespace SkateboardNeverDie.Services
{
    internal interface ISingleSignOnService
    {
        Task<TokenResponse> ClientCredentialsFlowAsync();
        Task<TokenResponse> RefreshTokenFlowAsync(string refreshToken);
        Task<TokenResponse> AuthorizationCodeFlowAsync();
        Task<bool> LogoutAsync(string identityToken);
        Task<UserInfo> UserInfoAsync(string accessToken);
    }
}
