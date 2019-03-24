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
        public static IApplicationBuilder UseSwagger(this IApplicationBuilder app, IWebAppConfiguration configuration, Action<SwaggerOptions> setupAction = null)
        {
            var swaggerDefinition = configuration.SwaggerDefinition;

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/{swaggerDefinition.Version}/swagger.json", swaggerDefinition.Title);
            });

            return app;
        }
    }
}
