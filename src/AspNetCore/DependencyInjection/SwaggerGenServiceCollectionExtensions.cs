using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Withywoods.AspNetCore.DependencyInjection
{
    /// <summary>
    /// Dependency injection extensions for Swagger generation.
    /// </summary>
    public static class SwaggerGenServiceCollectionExtensions
    {
        /// <summary>
        /// Add Swagger generation in ASP.NET Core dependency injection mechanism.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="openApi"></param>
        /// <param name="configCallback">Optional additionnal configuration for SwaggerGen</param>
        /// <returns></returns>
        public static IServiceCollection AddSwaggerGen(this IServiceCollection services, OpenApiInfo openApi, Action<SwaggerGenOptions> configCallback = null)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(openApi.Version, openApi);
            });
            return services;
        }
    }
}
