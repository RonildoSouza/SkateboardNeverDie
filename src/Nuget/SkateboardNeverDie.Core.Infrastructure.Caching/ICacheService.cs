using System;

namespace SkateboardNeverDie.Core.Infrastructure.Caching
{
    public interface ICacheService
    {
        void Add<TItem>(TItem item, string key, TimeSpan? expirationTime = null) where TItem : class;

        TItem Get<TItem>(string key) where TItem : class;

        void Remove(string key);
    }
}
