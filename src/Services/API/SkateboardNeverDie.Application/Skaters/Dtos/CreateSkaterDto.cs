using SkateboardNeverDie.Domain.Stances;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SkateboardNeverDie.Application.Skaters.Dtos
{
    public class CreateSkaterDto
    {
        [Required] public string FirstName { get; set; }
        [Required] public string LastName { get; set; }
        public string Nickname { get; set; }
        [Required] public DateTime Birthdate { get; set; }
        [Required] public StanceType NaturalStance { get; set; }
        [Required, MinLength(1)] public IEnumerable<SkaterTrickDto> SkaterTricks { get; set; }

        public class SkaterTrickDto
        {
            [Required] public Guid TrickId { get; set; }
            [Required, MinLength(1)] public IEnumerable<StanceType> Variations { get; set; }
        }
    }
}

