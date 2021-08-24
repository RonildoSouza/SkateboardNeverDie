using SkateboardNeverDie.Core.Domain;
using System;

namespace SkateboardNeverDie.Domain.Security
{
    public class UserPermission : Entity<Guid>
    {
        private UserPermission(Guid userId, string permissionId)
        {
            UserId = userId;
            PermissionId = permissionId;
        }

        protected UserPermission() { }

        public Guid UserId { get; private set; }
        public string PermissionId { get; private set; }

        public virtual User User { get; private set; }

        internal static UserPermission Create(Guid userId, string permissionId)
        {
            if (userId == default)
            {
                throw new ArgumentException($"The argument {nameof(userId)} can't be default Guid!");
            }

            if (string.IsNullOrEmpty(permissionId))
            {
                throw new ArgumentException($"The argument {nameof(permissionId)} can't be null or empty!");
            }

            return new UserPermission(userId, permissionId);
        }

        public static UserPermission Create(Guid id)
        {
            if (id == default)
            {
                throw new ArgumentException($"The argument {nameof(id)} can't be default Guid!");
            }

            return new UserPermission { Id = id };
        }
    }
}
