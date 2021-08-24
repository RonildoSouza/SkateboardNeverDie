using SkateboardNeverDie.Core.Domain;
using SkateboardNeverDie.Domain.Security.QueryData;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SkateboardNeverDie.Domain.Security
{
    public interface IPermissionRepository : IRepository<Permission>
    {
        Task<PagedResult<PermissionQueryData>> GetAllAsync(int page, int pageSize, CancellationToken cancelationToken = default);
        Task<PagedResult<UserPermissionQueryData>> GetUserPermissionsByPermissionIdAsync(string permissionId, int page, int pageSize, CancellationToken cancelationToken = default);
        void RemoveUserPermissionById(Guid userPermissionId);
    }
}
