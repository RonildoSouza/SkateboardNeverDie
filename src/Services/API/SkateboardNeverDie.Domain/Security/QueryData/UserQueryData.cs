using SkateboardNeverDie.Core.Domain;
using System;

namespace SkateboardNeverDie.Domain.Security.QueryData
{
    public class UserQueryData : IQueryData
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}