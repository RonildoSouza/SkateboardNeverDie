using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SkateboardNeverDie.Core.Domain;
using SkateboardNeverDie.Domain.Security.Services;
using SkateboardNeverDie.Domain.Skaters;
using SkateboardNeverDie.Infrastructure.Database;
using SkateboardNeverDie.Infrastructure.Domain.Security.Services;
using SkateboardNeverDie.Infrastructure.Domain.Skaters;
using System;
using System.Collections.Generic;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class InfrastructureServiceColletionExtension
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            // Sqlite Connection
            services.AddDbContext<ApplicationDbContext>(optionsBuilder =>
            {
                optionsBuilder.UseSqlite(connectionString, _ => _.MigrationsAssembly("SkateboardNeverDie.Infrastructure"));

#if DEBUG
                optionsBuilder.EnableSensitiveDataLogging();
                optionsBuilder.LogTo(Console.WriteLine, LogLevel.Debug);
#endif
            });

            // DI
            services.Scan(scan => scan
                .FromAssembliesOf(typeof(ISkaterRepository), typeof(SkaterRepository))
                    .AddClasses(_ => _.Where(type => type.Name.EndsWith("Repository")))
                        .AsImplementedInterfaces()
                        .WithScopedLifetime());

            services.Scan(scan => scan
                .FromAssembliesOf(typeof(IUserCacheService), typeof(UserCacheService))
                    .AddClasses(_ => _.Where(type => type.Name.EndsWith("Service")))
                        .AsImplementedInterfaces()
                        .WithScopedLifetime());

            services.AddScoped(typeof(IUnitOfWork), typeof(ApplicationDbContext));

            services.AddCacheService(new Dictionary<Type, TimeSpan>
            {
                { typeof(IdentityUserAuthorizeCache), TimeSpan.FromMinutes(30) }
            });

            return services;
        }
    }
}
