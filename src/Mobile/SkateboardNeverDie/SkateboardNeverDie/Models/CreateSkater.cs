using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SkateboardNeverDie.Models
{
    public sealed class CreateSkater
    {
        public CreateSkater(string firstName, string lastName, string nickname, DateTime birthdate, StanceType naturalStance)
        {
            SkaterTricks = new List<SkaterTrick>();
            FirstName = firstName;
            LastName = lastName;
            Nickname = nickname;
            Birthdate = birthdate;
            NaturalStance = naturalStance;
        }

        public string FirstName { get; }
        public string LastName { get; }
        public string Nickname { get; }
        public DateTime Birthdate { get; }
        public StanceType NaturalStance { get; }
        public List<SkaterTrick> SkaterTricks { get; }

        public sealed class SkaterTrick
        {
            public SkaterTrick()
            {
                Variations = new List<StanceType>();
            }

            public Guid TrickId { get; set; }
            public List<StanceType> Variations { get; set; }
        }
    }
}