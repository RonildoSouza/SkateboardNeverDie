using Microsoft.EntityFrameworkCore;
using SkateboardNeverDie.Core.Domain;
using System;

namespace SkateboardNeverDie.Core.Infrastructure
{
    public abstract class Repository<TAggregateRoot, TDbContext> : IRepository<TAggregateRoot>
        where TAggregateRoot : IAggregateRoot
        where TDbContext : DbContext, IUnitOfWork
    {
        public Repository(TDbContext context)
        {
            Context = context ?? throw new ArgumentNullException(typeof(TDbContext).Name);
        }

        public IUnitOfWork UnitOfWork => Context;
        protected TDbContext Context { get; }
    }
}
