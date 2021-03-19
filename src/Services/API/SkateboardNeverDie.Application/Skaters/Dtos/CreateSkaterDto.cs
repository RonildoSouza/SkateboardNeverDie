using SkateboardNeverDie.Domain.Stances;
using System;
using System.Collections.Generic;

namespace SkateboardNeverDie.Application.Skaters.Dtos
{
    public class CreateSkaterDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Nickname { get; set; }
        public DateTime Birthdate { get; set; }
        public StanceType NaturalStance { get; set; }
        public IEnumerable<SkaterTrickDto> SkaterTricks { get; set; }

        public class SkaterTrickDto
        {
            public Guid TrickId { get; set; }
            public IEnumerable<StanceType> Variations { get; set; }
        }
    }
}

