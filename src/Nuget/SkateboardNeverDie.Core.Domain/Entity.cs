namespace SkateboardNeverDie.Core.Domain
{
    public interface IEntity { }

    public abstract class Entity<TId> : IEntity
    {
        public TId Id { get; protected set; }
    }
}
