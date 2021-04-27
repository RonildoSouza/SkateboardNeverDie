using SkateboardNeverDie.Models;
using System.Threading.Tasks;

namespace SkateboardNeverDie.Services
{
    internal interface IAuthService
    {
        Task<TokenResponse> ClientCredentialsFlowAsync();
        Task<TokenResponse> RefreshTokenFlowAsync(string refreshToken);
        Task<TokenResponse> AuthorizationCodeFlowAsync();
        Task LogoutAsync();
    }
}
