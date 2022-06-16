using SkateboardNeverDie.Core.Domain;

namespace SkateboardNeverDie.Domain.Skaters.QueryData
{
    public class SkaterCountPerAgeQueryData : IQueryData
    {
        public int Count { get; set; }
        public int Age { get; set; }
    }
}