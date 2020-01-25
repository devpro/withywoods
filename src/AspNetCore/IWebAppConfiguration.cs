using Microsoft.OpenApi.Models;

namespace Withywoods.AspNetCore
{
    /// <summary>
    /// ASP.NET Core web application configuration.
    /// </summary>
    public interface IWebAppConfiguration
    {
        /// <summary>
        /// Swagger definition.
        /// </summary>
        OpenApiInfo SwaggerDefinition { get; }

        /// <summary>
        /// Web application assembly name.
        /// </summary>
        string AssemblyName { get; }
    }
}
