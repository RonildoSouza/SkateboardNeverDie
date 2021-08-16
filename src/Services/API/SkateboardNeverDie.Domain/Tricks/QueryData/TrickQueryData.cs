using SkateboardNeverDie.Core.Domain;
using System;

namespace SkateboardNeverDie.Domain.Tricks.QueryData
{
    public class TrickQueryData : IQueryData
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public static TrickQueryData Convert(Trick trick)
            => new()
            {
                Id = trick.Id,
                Name = trick.Name
            };
    }
}
