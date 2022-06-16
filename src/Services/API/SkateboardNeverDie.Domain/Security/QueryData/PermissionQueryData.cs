using SkateboardNeverDie.Core.Domain;

namespace SkateboardNeverDie.Domain.Security.QueryData
{
    public class PermissionQueryData : IQueryData
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}