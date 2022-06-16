using SkateboardNeverDie.Core.Domain;
using SkateboardNeverDie.Domain.Tricks;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SkateboardNeverDie.Domain.Skaters
{
    public class SkaterTrick : Entity<Guid>
    {
        private SkaterTrick(Guid trickId, List<SkaterTrickVariation> variations)
        {
            Id = Guid.NewGuid();
            TrickId = trickId;
            SkaterTrickVariations = variations;
        }

        protected SkaterTrick() { }

        public Guid TrickId { get; private set; }
        public virtual Trick Trick { get; private set; }
        public virtual List<SkaterTrickVariation> SkaterTrickVariations { get; private set; }

        internal static SkaterTrick Create(Guid trickId, List<SkaterTrickVariation> variations)
        {
            if (trickId == default)
            {
                throw new ArgumentException($"The argument {nameof(trickId)} can't be default Guid!");
            }

            if (!variations?.Any() ?? true)
            {
                throw new ArgumentException($"The argument {nameof(variations)} can't be null or empty!");
            }

            return new SkaterTrick(trickId, variations);
        }
    }
}
