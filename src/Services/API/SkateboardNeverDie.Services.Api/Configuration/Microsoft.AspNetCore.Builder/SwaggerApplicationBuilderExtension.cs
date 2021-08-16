using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Microsoft.AspNetCore.Builder
{
    public static class SwaggerApplicationBuilderExtension
    {
        public static IApplicationBuilder UseSwagger(this IApplicationBuilder app, IApiVersionDescriptionProvider provider)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                    options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());

                options.OAuthUsePkce();
                options.OAuthAppName("SkateboardNeverDie Single Sign-On");
                options.OAuthClientId("skateboard-api");

                options.DocExpansion(DocExpansion.None);
            });

            return app;
        }
    }
}
