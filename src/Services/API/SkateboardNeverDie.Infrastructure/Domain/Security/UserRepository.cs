using Microsoft.EntityFrameworkCore;
using SkateboardNeverDie.Core.Domain;
using SkateboardNeverDie.Core.Infrastructure;
using SkateboardNeverDie.Core.Infrastructure.Extensions;
using SkateboardNeverDie.Domain.Security;
using SkateboardNeverDie.Domain.Security.QueryData;
using SkateboardNeverDie.Domain.Security.Services;
using SkateboardNeverDie.Infrastructure.Database;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SkateboardNeverDie.Infrastructure.Domain.Security
{
    public sealed class UserRepository : Repository<User, ApplicationDbContext>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context)
            : base(context) { }

        public async Task<PagedResult<UserQueryData>> GetUsersByEmailAsync(string email, int page, int pageSize, CancellationToken cancelationToken = default)
        {
            return await Context.Users
                .AsNoTracking()
                .Where(_ => _.Email.StartsWith(email))
                .GetPagedResultAsync(
                page,
                pageSize,
                _ => new UserQueryData
                {
                    Id = _.Id,
                    Name = _.Name,
                    Email = _.Email
                },
                _ => _.Name,
                cancelationToken);
        }

        public async Task<IdentityUserAuthorizeCache> GetIdentityUserAuthorizeAsync(Guid identityUserId, CancellationToken cancelationToken = default)
        {
            return await Context.Users
                .AsNoTrackingWithIdentityResolution()
                .Include(_ => _.UserPermissions)
                .Where(_ => _.IdentityUserId == identityUserId)
                .Select(_ => new IdentityUserAuthorizeCache
                {
                    Name = _.Name,
                    Email = _.Email,
                    PermissionIds = _.UserPermissions.Select(up => up.PermissionId)
                })
                .FirstOrDefaultAsync(cancelationToken);
        }
    }
}
