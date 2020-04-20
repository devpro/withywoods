using System;
using Microsoft.AspNetCore.Builder;
using Swashbuckle.AspNetCore.Swagger;

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
        /// <param name="configuration"></param>
        /// <param name="setupAction"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseSwagger(this IApplicationBuilder app, IWebAppConfiguration configuration)
        {
            var swaggerDefinition = configuration.SwaggerDefinition;

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/{swaggerDefinition?.Version ?? "1.0"}/swagger.json", swaggerDefinition?.Title ?? "");
            });

            return app;
        }
    }
}
