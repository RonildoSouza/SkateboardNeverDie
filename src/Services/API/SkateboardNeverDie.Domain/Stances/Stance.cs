using SkateboardNeverDie.Core.Domain;

namespace SkateboardNeverDie.Domain.Stances
{
    public class Stance : Entity<StanceType>, IAggregateRoot
    {
        public Stance(StanceType id, string description)
        {
            Id = id;
            Description = description;
        }

        protected Stance() { }

        public string Description { get; private set; }
    }
}
