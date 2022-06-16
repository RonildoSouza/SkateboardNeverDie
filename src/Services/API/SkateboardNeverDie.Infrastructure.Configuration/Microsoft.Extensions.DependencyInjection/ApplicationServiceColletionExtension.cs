using SkateboardNeverDie.Application.Skaters;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ApplicationServiceColletionExtension
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // DI
            services.Scan(scan => scan
                .FromAssembliesOf(typeof(ISkaterAppService), typeof(ISkaterAppService))
                    .AddClasses(_ => _.Where(type => type.Name.EndsWith("AppService")))
                        .AsImplementedInterfaces()
                        .WithScopedLifetime());

            return services;
        }
    }
}
