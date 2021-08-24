using SkateboardNeverDie.Core.Domain;
using System;

namespace SkateboardNeverDie.Domain.Security.QueryData
{
    public class UserPermissionQueryData : IQueryData
    {
        public Guid Id { get; set; }
        public UserQueryData User { get; set; }
    }
}