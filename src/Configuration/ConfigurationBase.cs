using Microsoft.Extensions.Configuration;

namespace Withywoods.Configuration;

public abstract class ConfigurationBase(IConfiguration configuration)
{
    protected IConfiguration Configuration { get; } = configuration;

    protected IConfigurationSection TryGetSection(string sectionKey)
    {
        return Configuration.TryGetSection(sectionKey);
    }
}
