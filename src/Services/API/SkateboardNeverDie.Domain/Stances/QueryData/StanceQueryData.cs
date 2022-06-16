using SkateboardNeverDie.Core.Domain;

namespace SkateboardNeverDie.Domain.Stances.QueryData
{
    public class StanceQueryData : IQueryData
    {
        public StanceType Id { get; set; }
        public string Description { get; set; }
    }
}
