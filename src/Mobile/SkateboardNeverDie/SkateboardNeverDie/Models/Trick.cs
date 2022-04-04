using System;

namespace SkateboardNeverDie.Models
{
    public class Trick : IEquatable<Trick>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public bool Equals(Trick other) => other?.Id == Id;

        public struct Rels
        {
            public const string Create = "create-trick";
            public const string Delete = "delete-trick";
            public const string Next = "next";
        }
    }
}