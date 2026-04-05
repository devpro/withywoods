using System;
using Microsoft.Extensions.DependencyInjection;
using Withywoods.Twohire.Client.DependencyInjection;

namespace Withywoods.Twohire.Client.IntegrationTests.Sandbox;

public abstract class RepositoryIntegrationTestBase
{
    protected RepositoryIntegrationTestBase()
    {
        Configuration = new SandboxTwoHireRestApiConfiguration(Environment.GetEnvironmentVariable("Environment") ?? "Sandbox");

        var services = new ServiceCollection()
            .AddLogging()
            .AddTwohireClient(Configuration);
        ServiceProvider = services.BuildServiceProvider();
    }

    protected ServiceProvider ServiceProvider { get; private set; }

    protected SandboxTwoHireRestApiConfiguration Configuration { get; }
}
