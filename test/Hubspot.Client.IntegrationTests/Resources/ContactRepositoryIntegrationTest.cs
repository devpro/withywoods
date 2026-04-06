using System;
using System.Threading.Tasks;
using AwesomeAssertions;
using Withywoods.Hubspot.Client.Repositories;

namespace Withywoods.Hubspot.Client.IntegrationTests.Resources;

public class ContactRepositoryIntegrationTest()
    : RepositoryIntegrationTestBase<HubspotClientConfiguration, ContactRepository>(
        new HubspotClientConfiguration(Environment.GetEnvironmentVariable("Environment") ?? "Sandbox") { UseOAuth = false },
        (config, logger, httpClientFactory, tokenProvider) => new ContactRepository(config, logger, httpClientFactory, tokenProvider))
{
    // TODO: enable it
    // [Fact]
    public async Task ContactRepositorySandboxFindAllAsync_ReturnToken()
    {
        // Arrange
        var repository = BuildRepository();

        // Act
        var output = await repository.FindAllAsync();

        // Assert
        output.Should().NotBeNull();
    }
}
