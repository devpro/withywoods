using Microsoft.Extensions.Configuration;

namespace Withywoods.Configuration
{
    public abstract class ConfigurationBase
    {
        protected ConfigurationBase(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected IConfiguration Configuration { get; }

        protected IConfigurationSection TryGetSection(string sectionKey)
        {
            return Configuration.TryGetSection(sectionKey);
        }
    }
}
