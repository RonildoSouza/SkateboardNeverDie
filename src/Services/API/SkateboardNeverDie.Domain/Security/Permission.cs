using SkateboardNeverDie.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SkateboardNeverDie.Domain.Security
{
    public class Permission : Entity<string>, IAggregateRoot
    {
        protected Permission()
        {
            UserPermissions = new List<UserPermission>();
        }

        public string Name { get; private set; }
        public string Description { get; private set; }

        public virtual List<UserPermission> UserPermissions { get; private set; }

        public IEnumerable<Guid> AddUser(IEnumerable<Guid> userIds)
        {
            foreach (var userId in userIds)
            {
                if (userId == default || UserPermissions.Any(_ => _.UserId == userId && _.PermissionId == Id))
                    yield return Guid.Empty;

                var userPermission = UserPermission.Create(userId, Id);
                UserPermissions.Add(userPermission);

                yield return userPermission.Id;
            }
        }
    }
}
