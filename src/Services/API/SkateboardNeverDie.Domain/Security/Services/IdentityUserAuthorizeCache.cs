using System;
using System.Collections.Generic;
using System.Linq;

namespace SkateboardNeverDie.Domain.Security.Services
{
    public sealed class IdentityUserAuthorizeCache
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public IEnumerable<string> PermissionIds { get; set; }

        public bool HasPermission(string id) => PermissionIds?.Contains(id) ?? false;
    }
}
