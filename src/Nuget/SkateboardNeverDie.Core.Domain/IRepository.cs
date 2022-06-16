namespace SkateboardNeverDie.Core.Domain
{
    public interface IRepository<TAggregateRoot> where TAggregateRoot : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
