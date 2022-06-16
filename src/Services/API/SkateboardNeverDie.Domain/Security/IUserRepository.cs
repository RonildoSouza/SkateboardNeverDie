using SkateboardNeverDie.Core.Domain;
using SkateboardNeverDie.Domain.Security.QueryData;
using SkateboardNeverDie.Domain.Security.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SkateboardNeverDie.Domain.Security
{
    public interface IUserRepository : IRepository<User>
    {
        Task<PagedResult<UserQueryData>> GetUsersByEmailAsync(string email, int page, int pageSize, CancellationToken cancelationToken = default);
        Task<IdentityUserAuthorizeCache> GetIdentityUserAuthorizeAsync(Guid identityUserId, CancellationToken cancelationToken = default);
    }
}
