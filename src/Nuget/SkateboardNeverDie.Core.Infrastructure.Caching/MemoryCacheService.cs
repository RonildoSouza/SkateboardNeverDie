using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;

namespace SkateboardNeverDie.Core.Infrastructure.Caching
{
    public class MemoryCacheService : ICacheService
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IOptionsSnapshot<CacheSettings> _optionsSnapshotCacheSettings;

        public MemoryCacheService(IMemoryCache memoryCache, IOptionsSnapshot<CacheSettings> optionsSnapshotCacheSettings)
        {
            _memoryCache = memoryCache;
            _optionsSnapshotCacheSettings = optionsSnapshotCacheSettings;
        }

        public void Add<TItem>(TItem item, string key, TimeSpan? expirationTime = null) where TItem : class
        {
            var timespan = expirationTime ?? _optionsSnapshotCacheSettings.Value.GetExpiration(item.GetType());
            _memoryCache.Set(key, item, timespan);
        }

        public TItem Get<TItem>(string key) where TItem : class
        {
            if (_memoryCache.TryGetValue(key, out TItem value))
                return value;

            return null;
        }

        public void Remove(string key) => _memoryCache.Remove(key);
    }
}
