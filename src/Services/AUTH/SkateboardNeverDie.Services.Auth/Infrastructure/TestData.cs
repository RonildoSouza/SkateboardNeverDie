using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenIddict.Abstractions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SkateboardNeverDie.Services.Auth.Infrastructure
{
    public class TestData : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public TestData(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            await context.Database.EnsureCreatedAsync(cancellationToken);

            var manager = scope.ServiceProvider.GetRequiredService<IOpenIddictApplicationManager>();

            //if (await manager.FindByClientIdAsync("api-tester", cancellationToken) is null)
            //{
            //    await manager.CreateAsync(new OpenIddictApplicationDescriptor
            //    {
            //        ClientId = "api-tester",
            //        ClientSecret = "api-tester-secret",
            //        DisplayName = "API Tester",
            //        RedirectUris = { new Uri("https://oauth.pstmn.io/v1/callback") },
            //        Type = OpenIddictConstants.ClientTypes.Public,
            //        Permissions =
            //        {
            //            OpenIddictConstants.Permissions.Endpoints.Authorization,
            //            OpenIddictConstants.Permissions.Endpoints.Token,

            //            OpenIddictConstants.Permissions.GrantTypes.ClientCredentials,
            //            OpenIddictConstants.Permissions.GrantTypes.AuthorizationCode,
            //            OpenIddictConstants.Permissions.GrantTypes.RefreshToken,

            //            OpenIddictConstants.Permissions.Scopes.Email,
            //            OpenIddictConstants.Permissions.Scopes.Profile,
            //            OpenIddictConstants.Permissions.Scopes.Roles,
            //            $"{OpenIddictConstants.Permissions.Prefixes.Scope}skateboard-api",

            //            OpenIddictConstants.Permissions.ResponseTypes.Code
            //        }
            //    }, cancellationToken);
            //}

            if (await manager.FindByClientIdAsync("skateboard-api", cancellationToken) is null)
            {
                await manager.CreateAsync(new OpenIddictApplicationDescriptor
                {
                    ClientId = "skateboard-api",
                    ClientSecret = "YVqJpVvDso4hoZAy3XUmww==",
                    DisplayName = "Skateboard API",
                    Type = OpenIddictConstants.ClientTypes.Confidential,
                    Permissions =
                    {
                        OpenIddictConstants.Permissions.Endpoints.Token,

                        OpenIddictConstants.Permissions.GrantTypes.ClientCredentials,
                        OpenIddictConstants.Permissions.GrantTypes.RefreshToken,

                        $"{OpenIddictConstants.Permissions.Prefixes.Scope}skateboard-api",
                        $"{OpenIddictConstants.Permissions.Prefixes.Scope}skateboard-api.read",
                        $"{OpenIddictConstants.Permissions.Prefixes.Scope}skateboard-api.write",
                    }
                }, cancellationToken);
            }

            if (await manager.FindByClientIdAsync("skateboard-mobile", cancellationToken) is null)
            {
                await manager.CreateAsync(new OpenIddictApplicationDescriptor
                {
                    ClientId = "skateboard-mobile",
                    ClientSecret = "PVHcNBpJX9SmaSk2H+PGHw==",
                    DisplayName = "Skateboard Mobile",
                    Type = OpenIddictConstants.ClientTypes.Confidential,
                    Permissions =
                    {
                        OpenIddictConstants.Permissions.Endpoints.Token,

                        OpenIddictConstants.Permissions.GrantTypes.ClientCredentials,
                        OpenIddictConstants.Permissions.GrantTypes.RefreshToken,

                        OpenIddictConstants.Permissions.ResponseTypes.Code,

                        $"{OpenIddictConstants.Permissions.Prefixes.Scope}skateboard-api.read",
                    }
                }, cancellationToken);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
