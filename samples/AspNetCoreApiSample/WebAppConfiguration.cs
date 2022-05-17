using Microsoft.OpenApi.Models;
using Withywoods.Configuration;

namespace Withywoods.AspNetCoreApiSample
{
    internal class WebAppConfiguration : ConfigurationBase
    {
        public const string InMemoryDatabaseName = "TaskList";

        public const string HealthChecksEndpoint = "/health";

        public WebAppConfiguration(IConfiguration configuration)
            : base(configuration)
        {
        }

        public OpenApiInfo OpenApi => TryGetSection("ApiDefinition").Get<OpenApiInfo>();
    }
}
