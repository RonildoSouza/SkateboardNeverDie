using SkateboardNeverDie.Core.Domain;
using System;

namespace SkateboardNeverDie.Domain.Tricks.QueryData
{
    public class TrickQueryData : IQueryData
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
