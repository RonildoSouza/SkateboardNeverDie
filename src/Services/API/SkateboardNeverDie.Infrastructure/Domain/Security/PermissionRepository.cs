using Microsoft.EntityFrameworkCore;
using SkateboardNeverDie.Core.Domain;
using SkateboardNeverDie.Core.Infrastructure;
using SkateboardNeverDie.Core.Infrastructure.Extensions;
using SkateboardNeverDie.Domain.Security;
using SkateboardNeverDie.Domain.Security.QueryData;
using SkateboardNeverDie.Infrastructure.Database;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SkateboardNeverDie.Infrastructure.Domain.Security
{
    public sealed class PermissionRepository : Repository<Permission, ApplicationDbContext>, IPermissionRepository
    {
        public PermissionRepository(ApplicationDbContext context)
            : base(context) { }

        public async Task<PagedResult<PermissionQueryData>> GetAllAsync(int page, int pageSize, CancellationToken cancelationToken = default)
        {
            return await Context.Permissions
                .AsNoTracking()
                .GetPagedResultAsync(
                page,
                pageSize,
                _ => new PermissionQueryData
                {
                    Id = _.Id,
                    Name = _.Name,
                    Description = _.Description,
                },
                _ => _.Name,
                cancelationToken);
        }

        public async Task<PagedResult<UserPermissionQueryData>> GetUserPermissionsByPermissionIdAsync(string permissionId, int page, int pageSize, CancellationToken cancelationToken = default)
        {
            return await Context.UserPermissions
                .AsNoTrackingWithIdentityResolution()
                .Include(_ => _.User)
                .GetPagedResultAsync(
                page,
                pageSize,
                _ => new UserPermissionQueryData
                {
                    Id = _.Id,
                    User = new UserQueryData
                    {
                        Id = _.User.Id,
                        Name = _.User.Name,
                        Email = _.User.Email
                    }
                },
                _ => _.User.Name,
                cancelationToken);
        }

        public void RemoveUserPermissionById(Guid userPermissionId)
        {
            var userPermission = UserPermission.Create(userPermissionId);

            Context.Attach(userPermission);
            Context.UserPermissions.Remove(userPermission);
        }
    }
}
