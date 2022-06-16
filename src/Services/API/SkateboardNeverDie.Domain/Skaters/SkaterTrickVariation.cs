using SkateboardNeverDie.Core.Domain;
using SkateboardNeverDie.Domain.Stances;
using System;

namespace SkateboardNeverDie.Domain.Skaters
{
    public class SkaterTrickVariation : Entity<Guid>
    {
        private SkaterTrickVariation(StanceType stanceId)
        {
            Id = Guid.NewGuid();
            StanceId = stanceId;
        }

        private SkaterTrickVariation() { }

        public StanceType StanceId { get; private set; }
        public virtual Stance Stance { get; private set; }

        internal static SkaterTrickVariation Create(StanceType stanceId)
        {
            return new SkaterTrickVariation(stanceId);
        }
    }
}
