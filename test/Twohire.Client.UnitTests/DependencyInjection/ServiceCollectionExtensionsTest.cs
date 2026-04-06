using System.Net.Http;
using AwesomeAssertions;
using Microsoft.Extensions.DependencyInjection;
using Withywoods.Twohire.Abstractions.Providers;
using Withywoods.Twohire.Abstractions.Repositories;
using Withywoods.Twohire.Client.DependencyInjection;
using Xunit;

namespace Withywoods.Twohire.Client.UnitTests.DependencyInjection;

[Trait("Category", "UnitTests")]
public class ServiceCollectionExtensionsTest
{
    [Fact]
    public void AddTwohireClient_ShouldProvideRepositories()
    {
        // Arrange
        var serviceCollection = new ServiceCollection();
        var configuration = new FakeConfiguration();

        // Act
        serviceCollection.AddTwohireClient(configuration);

        // Assert
        var services = serviceCollection.BuildServiceProvider();
        services.GetRequiredService<IPersonalVehicleRepository>().Should().NotBeNull();
        services.GetRequiredService<ITokenRepository>().Should().NotBeNull();
    }

    [Fact]
    public void AddTwohireClient_ShouldProvideProviders()
    {
        // Arrange
        var serviceCollection = new ServiceCollection();
        var configuration = new FakeConfiguration();

        // Act
        serviceCollection.AddTwohireClient(configuration);

        // Assert
        var services = serviceCollection.BuildServiceProvider();
        services.GetRequiredService<ITokenProvider>().Should().NotBeNull();
    }

    [Fact]
    public void AddTwohireClient_ShouldProvideHttpClient()
    {
        // Arrange
        var serviceCollection = new ServiceCollection();
        var configuration = new FakeConfiguration();

        // Act
        serviceCollection.AddTwohireClient(configuration);

        // Assert
        var services = serviceCollection.BuildServiceProvider();
        var httpClientFactory = services.GetRequiredService<IHttpClientFactory>();
        httpClientFactory.Should().NotBeNull();
        var client = httpClientFactory.CreateClient(configuration.HttpClientName);
        client.Should().NotBeNull();
    }
}
