using Microsoft.OpenApi;
using Withywoods.Configuration;

namespace Withywoods.DemoWebApi;

public class ApplicationConfiguration(IConfigurationRoot configuration)
{
    public static ApplicationConfiguration Create(ConfigurationManager configuration)
    {
        return new ApplicationConfiguration(configuration);
    }

    public static string HealthCheckEndpoint => "/health";

    public bool IsHttpsRedirectionEnabled => configuration.TryGetSection<bool>("Features:IsHttpsRedirectionEnabled");

    public bool IsScalarEnabled => configuration.TryGetSection<bool>("Features:IsScalarEnabled");

    public OpenApiInfo OpenApiInfo => configuration.TryGetSection<OpenApiInfo>("OpenApi");
}
