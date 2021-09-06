using System;
using System.Threading;
using System.Threading.Tasks;

namespace SkateboardNeverDie.Domain.Security.Services
{
    public interface IUserCacheService
    {
        Task<IdentityUserAuthorizeCache> AddIdentityUserAuthorizeAsync(Guid identityUserId, CancellationToken cancellationToken = default);
        Task<IdentityUserAuthorizeCache> GetIdentityUserAuthorizeAsync(Guid identityUserId, CancellationToken cancellationToken = default);
    }
}
