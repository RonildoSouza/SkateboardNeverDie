using SkateboardNeverDie.Core.Infrastructure.Caching;
using SkateboardNeverDie.Domain.Security;
using SkateboardNeverDie.Domain.Security.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SkateboardNeverDie.Infrastructure.Domain.Security.Services
{
    public sealed class UserCacheService : IUserCacheService
    {
        private readonly IUserRepository _userRepository;
        private readonly ICacheService _cacheService;

        public UserCacheService(IUserRepository userRepository, ICacheService cacheService)
        {
            _userRepository = userRepository;
            _cacheService = cacheService;
        }

        public async Task<IdentityUserAuthorizeCache> AddIdentityUserAuthorizeAsync(Guid identityUserId, CancellationToken cancellationToken = default)
        {
            var identityUserAuthorize = await _userRepository.GetIdentityUserAuthorizeAsync(identityUserId, cancellationToken);
            _cacheService.Add(identityUserAuthorize, identityUserId.ToString());

            return identityUserAuthorize;
        }

        public async Task<IdentityUserAuthorizeCache> GetIdentityUserAuthorizeAsync(Guid identityUserId, CancellationToken cancellationToken = default)
        {
            var identityUserAuthorize = _cacheService.Get<IdentityUserAuthorizeCache>(identityUserId.ToString());

            if (identityUserAuthorize == null && identityUserId != Guid.Empty)
                identityUserAuthorize = await AddIdentityUserAuthorizeAsync(identityUserId, cancellationToken);

            return identityUserAuthorize;
        }
    }
}
