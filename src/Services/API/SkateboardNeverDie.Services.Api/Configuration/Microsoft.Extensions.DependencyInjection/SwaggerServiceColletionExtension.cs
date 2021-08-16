using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class SwaggerServiceColletionExtension
    {
        public static IServiceCollection AddSwaggerWithApiVersioning(this IServiceCollection services, IConfiguration configuration)
        {
            var openApiOAuthFlowSettings = configuration.GetSection(nameof(OpenApiOAuthFlowSettings)).Get<OpenApiOAuthFlowSettings>();

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

                var openApiOAuthFlow = new OpenApiOAuthFlow
                {
                    AuthorizationUrl = new Uri(openApiOAuthFlowSettings.AuthorizationUrl),
                    TokenUrl = new Uri(openApiOAuthFlowSettings.TokenUrl),
                    RefreshUrl = new Uri(openApiOAuthFlowSettings.RefreshUrl),
                    Scopes = openApiOAuthFlowSettings.Scopes.ToDictionary(k => k)
                };

                options.AddSecurityDefinition("OAuthFlow", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.OAuth2,
                    Scheme = "Bearer",
                    Flows = new OpenApiOAuthFlows
                    {
                        AuthorizationCode = openApiOAuthFlow,
                        ClientCredentials = openApiOAuthFlow
                    }
                });

                //options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                //{
                //    Description = "JWT Authorization header using the Bearer scheme. " +
                //                      "\r\n\r\n Enter 'Bearer' [space] and then your token in the text input below." +
                //                      "\r\n\r\nExample: \"Bearer eyJhbGciOiJSUzI1NiIs...\"",
                //    Name = "Authorization",
                //    In = ParameterLocation.Header,
                //    Type = SecuritySchemeType.ApiKey,
                //    Scheme = "Bearer"
                //});

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "OAuthFlow"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });
            });

            return services;
        }
    }

    public sealed class OpenApiOAuthFlowSettings
    {
        public string AuthorizationUrl { get; set; }
        public string TokenUrl { get; set; }
        public string RefreshUrl { get; set; }
        public IEnumerable<string> Scopes { get; set; }
    }
}
