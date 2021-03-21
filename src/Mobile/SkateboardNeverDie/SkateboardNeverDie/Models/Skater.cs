using System;

namespace SkateboardNeverDie.Models
{
    public class Skater
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Nickname { get; set; }
        public DateTime Birthdate { get; set; }
    }
}