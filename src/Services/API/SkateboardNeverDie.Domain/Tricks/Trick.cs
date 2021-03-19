using SkateboardNeverDie.Core.Domain;
using System;

namespace SkateboardNeverDie.Domain.Tricks
{
    public class Trick : Entity<Guid>, IAggregateRoot
    {
        public Trick(string name, string description)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
        }

        protected Trick() { }

        public string Name { get; private set; }
        public string Description { get; private set; }
    }
}
