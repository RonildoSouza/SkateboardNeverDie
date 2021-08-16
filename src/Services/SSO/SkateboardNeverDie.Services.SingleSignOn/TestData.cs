using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenIddict.Abstractions;
using SkateboardNeverDie.Services.SingleSignOn.Infrastructure;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SkateboardNeverDie.Services.SingleSignOn
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

            var openIddictApplicationManager = scope.ServiceProvider.GetRequiredService<IOpenIddictApplicationManager>();

            if (await openIddictApplicationManager.FindByClientIdAsync("skateboard-api", cancellationToken) is null)
            {
                await openIddictApplicationManager.CreateAsync(new OpenIddictApplicationDescriptor
                {
                    ClientId = "skateboard-api",
                    ClientSecret = "YVqJpVvDso4hoZAy3XUmww==",
                    DisplayName = "Skateboard API",
                    Type = OpenIddictConstants.ClientTypes.Confidential,
                    ConsentType = OpenIddictConstants.ConsentTypes.Explicit,
                    //PostLogoutRedirectUris = { new Uri("myapp://") },
                    RedirectUris =
                    {
                        new Uri("https://localhost:5001/swagger/oauth2-redirect.html"),
                        new Uri("https://skateboardneverdieservicesapi.azurewebsites.net/swagger/oauth2-redirect.html")
                    },
                    Permissions =
                    {
                        OpenIddictConstants.Permissions.Endpoints.Authorization,
                        //OpenIddictConstants.Permissions.Endpoints.Logout,
                        OpenIddictConstants.Permissions.Endpoints.Token,

                        OpenIddictConstants.Permissions.GrantTypes.AuthorizationCode,
                        OpenIddictConstants.Permissions.GrantTypes.ClientCredentials,
                        OpenIddictConstants.Permissions.GrantTypes.RefreshToken,

                        OpenIddictConstants.Permissions.ResponseTypes.Code,
                        OpenIddictConstants.Permissions.ResponseTypes.Token,

                        OpenIddictConstants.Permissions.Scopes.Email,
                        OpenIddictConstants.Permissions.Scopes.Profile,
                        OpenIddictConstants.Permissions.Scopes.Roles,
                        $"{OpenIddictConstants.Permissions.Prefixes.Scope}skateboard-api:read",
                    },
                    Requirements =
                    {
                        OpenIddictConstants.Requirements.Features.ProofKeyForCodeExchange
                    }
                }, cancellationToken);
            }

            if (await openIddictApplicationManager.FindByClientIdAsync("skateboard-mobile", cancellationToken) is null)
            {
                await openIddictApplicationManager.CreateAsync(new OpenIddictApplicationDescriptor
                {
                    ClientId = "skateboard-mobile",
                    ClientSecret = "PVHcNBpJX9SmaSk2H+PGHw==",
                    DisplayName = "Skateboard Mobile",
                    Type = OpenIddictConstants.ClientTypes.Confidential,
                    ConsentType = OpenIddictConstants.ConsentTypes.Implicit,
                    PostLogoutRedirectUris = { new Uri("myapp://") },
                    RedirectUris =
                    {
                        new Uri("myapp://"),
                        new Uri("https://localhost:5001/swagger/oauth2-redirect.html"),
                        new Uri("https://skateboardneverdieservicesapi.azurewebsites.net/swagger/oauth2-redirect.html")
                    },
                    Permissions =
                    {
                        OpenIddictConstants.Permissions.Endpoints.Authorization,
                        OpenIddictConstants.Permissions.Endpoints.Logout,
                        OpenIddictConstants.Permissions.Endpoints.Token,

                        OpenIddictConstants.Permissions.GrantTypes.AuthorizationCode,
                        OpenIddictConstants.Permissions.GrantTypes.ClientCredentials,
                        OpenIddictConstants.Permissions.GrantTypes.RefreshToken,

                        OpenIddictConstants.Permissions.ResponseTypes.Code,
                        OpenIddictConstants.Permissions.ResponseTypes.Token,

                        OpenIddictConstants.Permissions.Scopes.Email,
                        OpenIddictConstants.Permissions.Scopes.Profile,
                        OpenIddictConstants.Permissions.Scopes.Roles,
                        $"{OpenIddictConstants.Permissions.Prefixes.Scope}skateboard-api:read",
                    },
                    Requirements =
                    {
                        OpenIddictConstants.Requirements.Features.ProofKeyForCodeExchange
                    }
                }, cancellationToken);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
