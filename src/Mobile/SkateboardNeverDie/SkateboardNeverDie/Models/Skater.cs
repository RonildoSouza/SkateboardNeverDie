using System;

namespace SkateboardNeverDie.Models
{
    public sealed class Skater
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Nickname { get; set; }
        public DateTime Birthdate { get; set; }
        public StanceType NaturalStance { get; set; }

        public struct Rels
        {
            public const string Create = "create-skater";
            public const string Delete = "delete-skater";
        }
    }
}