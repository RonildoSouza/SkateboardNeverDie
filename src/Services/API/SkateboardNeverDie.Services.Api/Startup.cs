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
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.IO;
using System.IO.Compression;
using System.Net.Http;

namespace SkateboardNeverDie.Services.Api
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            Environment = env;

            Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json")
                .Build();
        }

        private IWebHostEnvironment Environment { get; }
        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddNewtonsoftJson(c =>
                {
                    c.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    c.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                });
            services.AddRouting(c =>
            {
                c.LowercaseUrls = true;
            });

            services.AddApiVersioning(c =>
            {
                c.ReportApiVersions = true;
            });

            services.AddVersionedApiExplorer(c =>
            {
                c.GroupNameFormat = "'v'VVV";
                c.SubstituteApiVersionInUrl = true;
            });
            services.AddSwaggerGen();

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
                .AddInfrastructure(Configuration.GetConnectionString("DefaultConnection"));

            services.AddSimpleHateoas();

            services.AddProblemDetails(options =>
            {
                options.IncludeExceptionDetails = (ctx, ex) => Environment.IsDevelopment();

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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    foreach (var description in provider.ApiVersionDescriptions)
                        c.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());

                    c.DocExpansion(DocExpansion.List);
                });
            }

            app.UseProblemDetails();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
