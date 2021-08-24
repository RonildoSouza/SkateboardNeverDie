using SkateboardNeverDie.Core.Domain;
using System;

namespace SkateboardNeverDie.Domain.Security
{
    public class User : Entity<Guid>, IAggregateRoot
    {
        public User(Guid identityUserId, string name, string email)
        {
            Id = Guid.NewGuid();
            IdentityUserId = identityUserId;
            Name = name;
            Email = email;
        }

        protected User() { }

        public Guid IdentityUserId { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
    }
}
