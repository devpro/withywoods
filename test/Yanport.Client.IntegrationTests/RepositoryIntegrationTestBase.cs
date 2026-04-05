using Devpro.Yanport.Client.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace Devpro.Yanport.Client.IntegrationTests
{
    public class RepositoryIntegrationTestBase<T> where T : class, IYanportClientConfiguration
    {
        protected RepositoryIntegrationTestBase(T configuration)
        {
            Configuration = configuration;

            var services = new ServiceCollection()
                .AddLogging()
                .AddYanportClient(Configuration);
            ServiceProvider = services.BuildServiceProvider();
        }

        protected ServiceProvider ServiceProvider { get; private set; }

        protected T Configuration { get; private set; }
    }
}
