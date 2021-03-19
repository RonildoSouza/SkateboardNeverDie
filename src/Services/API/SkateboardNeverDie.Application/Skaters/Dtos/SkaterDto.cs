using SkateboardNeverDie.Domain.Stances;
using System;

namespace SkateboardNeverDie.Application.Skaters.Dtos
{
    public class SkaterDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Nickname { get; set; }
        public DateTime Birthdate { get; set; }
        public StanceType NaturalStance { get; set; }
    }
}