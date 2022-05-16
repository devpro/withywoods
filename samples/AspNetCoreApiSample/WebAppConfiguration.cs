using Microsoft.OpenApi.Models;
using Withywoods.Configuration;

namespace Withywoods.AspNetCoreApiSample
{
    internal class WebAppConfiguration
    {
        public const string InMemoryDatabaseName = "TaskList";

        public const string HealthChecksEndpoint = "/health";

        private readonly IConfiguration _configuration;

        public WebAppConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public OpenApiInfo OpenApi => _configuration.TryGetSection("ApiDefinition").Get<OpenApiInfo>();
    }
}
