using System;
using System.Collections.Generic;

namespace SkateboardNeverDie.Models
{
    public sealed class SkaterTrick : IEquatable<SkaterTrick>
    {
        public Guid Id { get; set; }
        public string TrickName { get; set; }
        public IEnumerable<StanceType> TrickVariations { get; set; }

        public string TrickVariationsToString => string.Join(", ", TrickVariations);

        public bool Equals(SkaterTrick other) => other?.Id == Id;

        public struct Rels
        {
            public const string Next = "next";
        }
    }
}