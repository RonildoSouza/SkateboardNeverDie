using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using OpenIddict.Abstractions;
using SkateboardNeverDie.Services.Auth.Infrastructure;
using System;

namespace SkateboardNeverDie.Services.Auth
{
    public class Startup
    {
        private readonly IWebHostEnvironment _environment;
        private readonly IConfiguration _configuration;

        public Startup(IWebHostEnvironment env)
        {
            _environment = env;

            _configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true)
                .Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddCors();

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlite(_configuration.GetConnectionString("DefaultConnection"));

                // Register the entity sets needed by OpenIddict
                options.UseOpenIddict<Guid>();
            });

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddIdentity<ApplicationUser, IdentityRole>()
                    .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddDefaultTokenProviders()
                    .AddDefaultUI();

            // Configure Identity to use the same JWT claims as OpenIddict instead
            // of the legacy WS-Federation claims it uses by default (ClaimTypes),
            // which saves you from doing the mapping in your authorization controller.
            services.Configure<IdentityOptions>(options =>
            {
                options.ClaimsIdentity.UserNameClaimType = OpenIddictConstants.Claims.Name;
                options.ClaimsIdentity.UserIdClaimType = OpenIddictConstants.Claims.Subject;
                options.ClaimsIdentity.RoleClaimType = OpenIddictConstants.Claims.Role;

                // Note: to require account confirmation before login,
                // register an email sender service (IEmailSender) and
                // set options.SignIn.RequireConfirmedAccount to true.
                //
                // For more information, visit https://aka.ms/aspaccountconf.
                options.SignIn.RequireConfirmedAccount = false;
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(options =>
                    {
                        options.LoginPath = "/identity/account/login";
                        options.LogoutPath = "/identity/account/logout";
                    });

            services.AddOpenIddict()

                // Register the OpenIddict core components.
                .AddCore(options =>
                {
                    // Configure OpenIddict to use the Entity Framework Core stores and models.
                    // Note: call ReplaceDefaultEntities() to replace the default entities.
                    options.UseEntityFrameworkCore()
                           .UseDbContext<ApplicationDbContext>()
                           .ReplaceDefaultEntities<Guid>();
                })

                // Register the OpenIddict server components.
                .AddServer(options =>
                {
                    // Enable the token endpoint.
                    options.SetAuthorizationEndpointUris("/connect/authorize")
                           .SetLogoutEndpointUris("/connect/logout")
                           .SetTokenEndpointUris("/connect/token")
                           .SetUserinfoEndpointUris("/connect/userinfo");

                    // Enable flows.
                    options.AllowClientCredentialsFlow()
                           .AllowAuthorizationCodeFlow()//.RequireProofKeyForCodeExchange()
                           .AllowRefreshTokenFlow();

                    // Register the signing and encryption credentials.
                    options.AddEphemeralEncryptionKey()
                           .AddEphemeralSigningKey();

                    // Register scopes (permissions)
                    options.RegisterScopes(
                        OpenIddictConstants.Scopes.Email,
                        OpenIddictConstants.Scopes.Profile,
                        OpenIddictConstants.Scopes.Roles,
                        "skateboard-api",
                        "skateboard-api.read",
                        "skateboard-api.write");

                    // Register the ASP.NET Core host and configure the ASP.NET Core options.
                    options.UseAspNetCore()
                           .EnableTokenEndpointPassthrough()
                           .EnableAuthorizationEndpointPassthrough()
                           .EnableLogoutEndpointPassthrough()
                           .EnableUserinfoEndpointPassthrough()
                           .EnableStatusCodePagesIntegration();

                    if (_environment.IsDevelopment())
                    {
                        options.AddEphemeralEncryptionKey()
                               .AddEphemeralSigningKey()
                               .DisableAccessTokenEncryption()
                               .SetIssuer(new Uri("https://localhost:5003"));
                    }
                    else
                    {
                        options.AddEncryptionKey(new SymmetricSecurityKey(Convert.FromBase64String("PsL3WtvyJY5XLDVtwew/2GWG77FF29eiLsgXuVCYEge2VIFE4dmHVBu72JsApl1RqFCiguIl2lLEOHN9QoWfyQ==")));
                        options.AddSigningKey(new SymmetricSecurityKey(Convert.FromBase64String("6TydNhReMOYyAsRcgh+ep/6mIoBbZEecoqtB9XVxreYxFcgXE4EUNmhAct1U83+VQYb8DnkaUdUPeOtfhciPWw==")));
                    }
                })

                .AddValidation();

            // Register the worker responsible of seeding the database with the sample clients.
            // Note: in a real world application, this step should be part of a setup script.
            services.AddHostedService<TestData>();

            // Added after AddMvc()
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = $"/account/login";
                options.LogoutPath = $"/account/logout";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseStatusCodePagesWithReExecute("~/error");

                //app.UseExceptionHandler("/Error");
                //app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseCors(builder =>
            {
                builder.WithOrigins("*"/*, "https://localhost:5001"*/);
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
            });

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(options =>
            {
                options.MapControllers();
                options.MapDefaultControllerRoute();
                options.MapRazorPages();
            });
        }
    }
}
