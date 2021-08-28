using SkateboardNeverDie.Core.Infrastructure.Caching;
using System;
using System.Collections.Generic;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceColletionExtension
    {
        public static IServiceCollection AddCacheService(this IServiceCollection services, Dictionary<Type, TimeSpan> expirations)
        {
            services.AddMemoryCache();

            services.AddScoped<ICacheService, MemoryCacheService>();

            CacheSettings.AddExpiration(expirations);

            return services;
        }
    }
}
