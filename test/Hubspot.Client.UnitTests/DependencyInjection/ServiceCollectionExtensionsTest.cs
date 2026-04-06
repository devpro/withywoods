using System.Net.Http;
using AwesomeAssertions;
using Microsoft.Extensions.DependencyInjection;
using Withywoods.Hubspot.Abstractions.Providers;
using Withywoods.Hubspot.Client.DependencyInjection;
using Xunit;

namespace Withywoods.Hubspot.Client.UnitTests.DependencyInjection;

[Trait("Category", "UnitTests")]
public class ServiceCollectionExtensionsTest
{
    private readonly DefaultHubspotClientConfiguration _configuration = new()
    {
        BaseUrl = "https://sure.dont.exist",
        HttpClientName = "Fake",
        ApiKey = "Fake",
        ApplicationId = "Fake",
        ClientId = "Fake",
        ClientSecret = "Fake",
        RedirectUrl = "Fake"
    };

    [Fact]
    public void AddHubspotClient_ShouldProvideRepositories()
    {
        // Arrange
        var serviceCollection = new ServiceCollection()
            .AddLogging();

        // Act
        serviceCollection.AddHubspotClient(_configuration);

        // Assert
        var services = serviceCollection.BuildServiceProvider();
        services.GetRequiredService<Abstractions.Repositories.IContactRepository>().Should().NotBeNull();
    }

    [Fact]
    public void AddHubspotClient_ShouldProvideProviders()
    {
        // Arrange
        var serviceCollection = new ServiceCollection();

        // Act
        serviceCollection.AddHubspotClient(_configuration);

        // Assert
        var services = serviceCollection.BuildServiceProvider();
        services.GetRequiredService<ITokenProvider>().Should().NotBeNull();
    }

    [Fact]
    public void AddHubspotClient_ShouldProvideHttpClient()
    {
        // Arrange
        var serviceCollection = new ServiceCollection()
            .AddLogging();

        // Act
        serviceCollection.AddHubspotClient(_configuration);

        // Assert
        var services = serviceCollection.BuildServiceProvider();
        var httpClientFactory = services.GetRequiredService<IHttpClientFactory>();
        httpClientFactory.Should().NotBeNull();
        var client = httpClientFactory.CreateClient(_configuration.HttpClientName);
        client.Should().NotBeNull();
    }
}
