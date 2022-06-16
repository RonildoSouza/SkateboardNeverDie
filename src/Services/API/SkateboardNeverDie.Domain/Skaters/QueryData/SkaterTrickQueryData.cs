using SkateboardNeverDie.Core.Domain;
using SkateboardNeverDie.Domain.Stances;
using System;
using System.Collections.Generic;

namespace SkateboardNeverDie.Domain.Skaters.QueryData
{
    public class SkaterTrickQueryData : IQueryData
    {
        public Guid Id { get; set; }
        public string TrickName { get; set; }
        public IEnumerable<StanceType> TrickVariations { get; set; }
    }
}