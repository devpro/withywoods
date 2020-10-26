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
        /// <returns></returns>
        public static IServiceCollection AddSwaggerGen(this IServiceCollection services, IWebAppConfiguration configuration)
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
            });

            return services;
        }

        /// <summary>
        /// Add Swagger generation in ASP.NET Core dependency injection mechanism, with Basic Authentication mechanism.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddSwaggerGenWithBasicAuthSecurity(this IServiceCollection services, IWebAppConfiguration configuration)
        {
            var swaggerDefinition = configuration.SwaggerDefinition;

            services.AddSwaggerGen(c =>
            {
                var xmlFile = $"{configuration.AssemblyName}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                c.SwaggerDoc(swaggerDefinition.Version, swaggerDefinition);
                c.IncludeXmlComments(xmlPath);
                c.CustomSchemaIds(GetClassNameWithoutDtoSuffix);
                c.AddSecurityDefinition("BasicAuthentication", new OpenApiSecurityScheme() { Scheme = "Basic", In=ParameterLocation.Header, Name= "Authorization", Type=SecuritySchemeType.Http });
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
