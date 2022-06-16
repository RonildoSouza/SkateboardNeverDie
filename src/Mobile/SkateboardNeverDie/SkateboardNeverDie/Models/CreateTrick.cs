namespace SkateboardNeverDie.Models
{
    public sealed class CreateTrick
    {
        public CreateTrick(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public string Name { get; }
        public string Description { get; }
    }
}