using SkateboardNeverDie.Core.Domain;
using SkateboardNeverDie.Domain.Stances;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SkateboardNeverDie.Domain.Skaters
{
    public class Skater : Entity<Guid>, IAggregateRoot
    {
        public Skater(string firstName, string lastName, string nickname, DateTime birthdate, StanceType naturalStanceId)
        {
            Id = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
            Nickname = nickname;
            Birthdate = birthdate;
            NaturalStanceId = naturalStanceId;

            SkaterTricks = new List<SkaterTrick>();
        }

        protected Skater() { }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Nickname { get; private set; }
        public DateTime Birthdate { get; private set; }

        public StanceType NaturalStanceId { get; private set; }
        public virtual Stance NaturalStance { get; private set; }
        public virtual List<SkaterTrick> SkaterTricks { get; private set; }

        public Guid AddTrick(Guid trickId, IEnumerable<StanceType> stances)
        {
            if (stances == null || !stances.Any())
                return Guid.Empty;

            var variations = new List<SkaterTrickVariation>();

            foreach (var stance in stances)
                variations.Add(SkaterTrickVariation.Create(stance));

            var skaterTrick = SkaterTrick.Create(trickId, variations);
            SkaterTricks.Add(skaterTrick);

            return skaterTrick.Id;
        }
    }
}
