using System;
using Microsoft.Extensions.DependencyInjection;
using Withywoods.Yanport.Client.DependencyInjection;

namespace Withywoods.Yanport.Client.IntegrationTests;

public abstract class RepositoryIntegrationTestBase
{
    protected RepositoryIntegrationTestBase()
    {
        Configuration = new YanportClientConfiguration(Environment.GetEnvironmentVariable("Environment") ?? "Sandbox");

        var services = new ServiceCollection()
            .AddLogging()
            .AddYanportClient(Configuration);
        ServiceProvider = services.BuildServiceProvider();
    }

    protected ServiceProvider ServiceProvider { get; private set; }

    protected IYanportClientConfiguration Configuration { get; }
}
