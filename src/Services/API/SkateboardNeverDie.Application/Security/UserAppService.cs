using SkateboardNeverDie.Domain.Security.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SkateboardNeverDie.Application.Security
{
    public sealed class UserAppService : IUserAppService
    {
        private readonly IUserCacheService _userCacheService;

        public UserAppService(IUserCacheService userCacheService)
        {
            _userCacheService = userCacheService;
        }

        public async Task AuthorizeAsync(Guid identityUserId, CancellationToken cancelationToken = default)
        {
            await _userCacheService.AddIdentityUserAuthorizeAsync(identityUserId, cancelationToken);
        }
    }
}
