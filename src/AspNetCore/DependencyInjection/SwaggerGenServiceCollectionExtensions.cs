using System;
using System.IO;
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
        private const string DtoClassPrefix = "Dto";

        /// <summary>
        /// Add Swagger generation in ASP.NET Core dependency injection mechanism.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <param name="configCallback">Optional additionnal configuration for SwaggerGen</param>
        /// <returns></returns>
        public static IServiceCollection AddSwaggerGen(this IServiceCollection services, IWebAppConfiguration configuration, Action<SwaggerGenOptions> configCallback = null)
        {
            var swaggerDefinition = configuration.SwaggerDefinition;

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(swaggerDefinition.Version, swaggerDefinition);

                // idea: manage Bearer authentication

                var xmlFile = $"{configuration.AssemblyName}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
                c.CustomSchemaIds(GetClassNameWithoutDtoSuffix);
                if(!configCallback is null)
                {
                    configCallback(c);
                }
            });

            return services;
        }

        /// <summary>
        /// Get the class name without Dto suffix.
        /// </summary>
        /// <param name="classType"></param>
        /// <returns></returns>
        private static string GetClassNameWithoutDtoSuffix(Type classType)
        {
            // idea: read a prefix list in the configuration so it can be defined at application level

            var returnedValue = classType.Name;
            if (returnedValue.EndsWith(DtoClassPrefix, StringComparison.InvariantCultureIgnoreCase))
                returnedValue = returnedValue.Replace(DtoClassPrefix, string.Empty, StringComparison.InvariantCultureIgnoreCase);

            return returnedValue;
        }
    }
}
