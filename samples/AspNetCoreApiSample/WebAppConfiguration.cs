using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using Withywoods.AspNetCore;
using Withywoods.Configuration;

namespace Withywoods.AspNetCoreApiSample
{
    internal class WebAppConfiguration : IWebAppConfiguration
    {
        public const string InMemoryDatabaseName = "TaskList";

        public const string HealthChecksEndpoint = "/health";

        private readonly IConfiguration _configuration;

        public WebAppConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        OpenApiInfo IWebAppConfiguration.SwaggerDefinition => _configuration.TryGetSection("ApiDefinition").Get<OpenApiInfo>();

        string IWebAppConfiguration.AssemblyName => typeof(Program).GetTypeInfo().Assembly.GetName().Name ?? "Withywoods.AspNetCoreApiSample";
    }
}
