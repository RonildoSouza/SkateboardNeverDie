using System.ComponentModel.DataAnnotations;

namespace SkateboardNeverDie.Application.Tricks.Dtos
{
    public class CreateTrickDto
    {
        [Required] public string Name { get; set; }
        public string Description { get; set; }
    }
}
