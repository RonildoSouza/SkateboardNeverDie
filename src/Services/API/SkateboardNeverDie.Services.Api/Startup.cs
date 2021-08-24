using FluentValidation;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using OpenIddict.Abstractions;
using OpenIddict.Validation.AspNetCore;
using SkateboardNeverDie.Services.Api.Settings;
using System;
using System.IO.Compression;
using System.Net.Http;

namespace SkateboardNeverDie.Services.Api
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
            var singleSignOnSettigns = _configuration.GetSection(nameof(SingleSignOnSettings)).Get<SingleSignOnSettings>();

            services.AddCors();
            services.AddRouting(options =>
            {
                options.LowercaseUrls = true;
            })
            .AddControllers()
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.Converters.Add(new StringEnumConverter());
            });

            if (_environment.IsDevelopment())
            {
                services.AddDatabaseDeveloperPageExceptionFilter();
            }

            services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Optimal;
            })
            .AddResponseCompression(options =>
            {
                options.Providers.Add<GzipCompressionProvider>();
                options.EnableForHttps = true;
            });

            services.AddSwaggerWithApiVersioning(_configuration);

            services
                .AddApplication()
                .AddInfrastructure(_configuration.GetConnectionString("DefaultConnection"));

            services.AddSimpleHateoas();

            services.AddProblemDetails(options =>
            {
                options.IncludeExceptionDetails = (ctx, ex) => _environment.IsDevelopment();

                // 4xx
                options.Map<ValidationException>(ex => new ProblemDetails
                {
                    Title = "Validation error",
                    Status = StatusCodes.Status412PreconditionFailed,
                    Detail = ex.Message,
                    Type = "https://httpstatuses.com/412"
                });

                // 5xx
                options.MapToStatusCode<NotImplementedException>(StatusCodes.Status501NotImplemented);
                options.MapToStatusCode<HttpRequestException>(StatusCodes.Status503ServiceUnavailable);
                options.MapToStatusCode<Exception>(StatusCodes.Status500InternalServerError);
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Read", policy =>
                {
                    policy.RequireAssertion(context =>
                    {
                        return context.User.HasScope("skateboard-api:read");
                    });
                });

                options.AddPolicy("Skaters:Write", policy =>
                {
                    policy.RequireAssertion(context =>
                    {
                        // TODO: Check permissions in memory storage
                        return context.User.HasClaim(OpenIddictConstants.Claims.Role, "admin") || context.User.HasScope("skateboard-api:read");
                    });
                });
            });

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme;
            });

            // Register the OpenIddict validation components.
            services.AddOpenIddict()
                .AddValidation(options =>
                {
                    // Note: the validation handler uses OpenID Connect discovery
                    // to retrieve the address of the introspection endpoint.
                    options.SetIssuer(singleSignOnSettigns.Issuer);

                    // Register the System.Net.Http integration.
                    options.UseSystemNetHttp();

                    // Register the ASP.NET Core host.
                    options.UseAspNetCore();
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IApiVersionDescriptionProvider provider)
        {
            if (_environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger(provider);

            app.UseProblemDetails();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(builder =>
            {
                builder.WithOrigins("*");
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
            });

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
