using System;

namespace SkateboardNeverDie.Models
{
    public sealed class Trick
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public struct Rels
        {
            public const string Create = "create-trick";
        }
    }
}