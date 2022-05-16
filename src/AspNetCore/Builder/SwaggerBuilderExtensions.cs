using Microsoft.AspNetCore.Builder;
using Microsoft.OpenApi.Models;

namespace Withywoods.AspNetCore.Builder
{
    /// <summary>
    /// Builder extensions for Swagger.
    /// </summary>
    public static class SwaggerBuilderExtensions
    {
        /// <summary>
        /// Use Swagger.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="openApi"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseSwagger(this IApplicationBuilder app, OpenApiInfo openApi)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options => options.SwaggerEndpoint($"/swagger/{openApi.Version}/swagger.json", $"{openApi.Title} {openApi.Version}"));
            return app;
        }
    }
}
