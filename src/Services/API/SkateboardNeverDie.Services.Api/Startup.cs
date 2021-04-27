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
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using OpenIddict.Abstractions;
using OpenIddict.Validation.AspNetCore;
using SkateboardNeverDie.Services.Api.Settings;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;
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
            var authSettigns = _configuration.GetSection(nameof(AuthSettings)).Get<AuthSettings>();

            services.AddCors();
            services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                });

            services.AddRouting(options =>
            {
                options.LowercaseUrls = true;
            });

            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
            });

            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Skateboard Never Die API", Version = "v1.0" });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. " +
                                      "\r\n\r\n Enter 'Bearer' [space] and then your token in the text input below." +
                                      "\r\n\r\nExample: \"Bearer eyJhbGciOiJSUzI1NiIs...\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });

                //options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                //{
                //    Name = "Authorization",
                //    In = ParameterLocation.Header,
                //    Type = SecuritySchemeType.OAuth2,
                //    Scheme = "Bearer",
                //    Flows = new OpenApiOAuthFlows
                //    {
                //        ClientCredentials = new OpenApiOAuthFlow
                //        {
                //            TokenUrl = new Uri($"{authSettigns.Url}/connect/token"),
                //            Scopes = new Dictionary<string, string>()
                //            {
                //                { "skateboard-api.read", "skateboard-api.read" },
                //                { "skateboard-api.write", "skateboard-api.write" },
                //            }
                //        }
                //    }
                //});

                //options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                //{
                //    {
                //        new OpenApiSecurityScheme
                //        {
                //            Reference = new OpenApiReference
                //            {
                //                Type = ReferenceType.SecurityScheme,
                //                Id = "oauth2"
                //            },

                //        },
                //        new List<string>()
                //    }
                //});
            });

            services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Optimal;
            })
            .AddResponseCompression(options =>
            {
                options.Providers.Add<GzipCompressionProvider>();
                options.EnableForHttps = true;
            });

            services
                .AddApplication()
                .AddInfrastructure(_configuration.GetConnectionString("DefaultConnection"));

            services.AddDatabaseDeveloperPageExceptionFilter();

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
                        return context.User.HasScope("skateboard-api.read");
                    });
                });

                options.AddPolicy("Write", policy =>
                {
                    //policy.RequireClaim(OpenIddictConstants.Claims.Role, "admin");
                    policy.RequireAssertion(context =>
                    {
                        return context.User.HasScope("skateboard-api.write") && context.User.HasScope("skateboard-api.read");
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
                    options.SetIssuer(authSettigns.Url);

                    //// Configure the validation handler to use introspection and register the client
                    //// credentials used when communicating with the remote introspection endpoint.
                    //options
                    //    .UseIntrospection()
                    //    .SetClientId("api-skateboard")
                    //    .SetClientSecret("YVqJpVvDso4hoZAy3XUmww==");

                    // Register the System.Net.Http integration.
                    options.UseSystemNetHttp();

                    // Register the ASP.NET Core host.
                    options.UseAspNetCore();
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    foreach (var description in provider.ApiVersionDescriptions)
                        options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());

                    options.DocExpansion(DocExpansion.List);
                });
            }

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
