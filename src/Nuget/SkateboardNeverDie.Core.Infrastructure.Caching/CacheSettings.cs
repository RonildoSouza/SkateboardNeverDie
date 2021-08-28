using System;
using System.Collections.Generic;

namespace SkateboardNeverDie.Core.Infrastructure.Caching
{
    public sealed class CacheSettings
    {
        private static readonly Dictionary<string, TimeSpan> _expirations = new Dictionary<string, TimeSpan>();

        public double ExpirationInMinutes { get; set; }

        internal static void AddExpiration(Dictionary<Type, TimeSpan> expirations)
        {
            _expirations.Clear();

            foreach (var expiration in expirations)
                _expirations.TryAdd(expiration.Key.Name, expiration.Value);
        }

        internal TimeSpan GetExpiration(Type itemType)
        {
            if (_expirations.TryGetValue(itemType.Name, out TimeSpan timeSpan))
                return timeSpan;

            return TimeSpan.FromMinutes(ExpirationInMinutes);
        }
    }
}
