using System;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Withywoods.AspNetCore.DependencyInjection
{
    /// <summary>
    /// Dependency injection extensions for Swagger generation.
    /// </summary>
    public static class SwaggerGenServiceCollectionExtensions
    {
        private const string _DtoClassPrefix = "Dto";

        /// <summary>
        /// Add Swagger generation in ASP.NET Core dependency injection mechanism.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddSwaggerGen(this IServiceCollection services, IWebAppConfiguration configuration)
        {
            var swaggerDefinition = configuration.SwaggerDefinition;

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(swaggerDefinition.Version, swaggerDefinition);

                var xmlFile = $"{configuration.AssemblyName}.xml";
                var xmlPath = System.IO.Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
                c.CustomSchemaIds(GetClassNameWithoutDtoSuffix);
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
            if (returnedValue.EndsWith(_DtoClassPrefix, StringComparison.InvariantCultureIgnoreCase))
                returnedValue = returnedValue.Replace(_DtoClassPrefix, string.Empty, StringComparison.InvariantCultureIgnoreCase);

            return returnedValue;
        }
    }
}
