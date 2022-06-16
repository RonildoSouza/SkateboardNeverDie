using Microsoft.AspNetCore.Authorization;
using SkateboardNeverDie.Services.Api.AuthorizationRequirements;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class AuthorizationServiceColletionExtension
    {
        public static IServiceCollection AddCustomAuthorization(this IServiceCollection services)
        {
            // DI
            services.Scan(scan => scan
                .FromCallingAssembly()
                    .AddClasses(_ => _.Where(type => type.Name.EndsWith("Handler")))
                        .As<IAuthorizationHandler>()
                        .WithScopedLifetime());

            services.AddHttpContextAccessor();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Read", policy =>
                {
                    policy.AddRequirements(ScopeRequirement.Scopes.SkateboardApiRead);
                });

                #region Skaters Permissions
                options.AddPolicy("Skaters:Add", policy =>
                {
                    policy.AddRequirements(
                        ScopeRequirement.Scopes.SkateboardApiRead,
                        PermissionRequirement.Permissions.Skaters.Add);
                });

                options.AddPolicy("Skaters:Edit", policy =>
                {
                    policy.AddRequirements(
                        ScopeRequirement.Scopes.SkateboardApiRead,
                        PermissionRequirement.Permissions.Skaters.Edit);
                });

                options.AddPolicy("Skaters:Remove", policy =>
                {
                    policy.AddRequirements(
                        ScopeRequirement.Scopes.SkateboardApiRead,
                        PermissionRequirement.Permissions.Skaters.Remove);
                });
                #endregion

                #region Tricks Permissions
                options.AddPolicy("Tricks:Add", policy =>
                {
                    policy.AddRequirements(
                        ScopeRequirement.Scopes.SkateboardApiRead,
                        PermissionRequirement.Permissions.Tricks.Add);
                });

                options.AddPolicy("Tricks:Edit", policy =>
                {
                    policy.AddRequirements(
                        ScopeRequirement.Scopes.SkateboardApiRead,
                        PermissionRequirement.Permissions.Tricks.Edit);
                });

                options.AddPolicy("Tricks:Remove", policy =>
                {
                    policy.AddRequirements(
                        ScopeRequirement.Scopes.SkateboardApiRead,
                        PermissionRequirement.Permissions.Tricks.Remove);
                });
                #endregion
            });

            return services;
        }
    }
}
