using System;
using System.Net.Http;
using System.Threading.Tasks;
using AwesomeAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Withywoods.Twohire.Abstractions.Providers;
using Withywoods.Twohire.Abstractions.Repositories;
using Withywoods.Twohire.Client.Repositories;

namespace Withywoods.Twohire.Client.IntegrationTests.Resources;

public class PersonalVehicleRepositoryIntegrationTest : RepositoryIntegrationTestBase
{
    // TODO: enable it
    // [Fact]
    public async Task PersonalVehicleRepositorySandboxFindAllAsync_ReturnToken()
    {
        // Arrange
        var repository = BuildRepository();

        // Act
        var output = await repository.FindAllAsync();

        // Assert
        output.Should().NotBeNull();
        output.Error.Should().BeNull();
        output.Status.Should().BeTrue();
        output.Data.Should().NotBeEmpty();
    }

    private IPersonalVehicleRepository BuildRepository()
    {
        var logger = ServiceProvider.GetService<ILogger<PersonalVehicleRepository>>() ?? throw new NullReferenceException();
        var httpClientFactory = ServiceProvider.GetService<IHttpClientFactory>() ?? throw new NullReferenceException();
        var tokenProvider = ServiceProvider.GetService<ITokenProvider>() ?? throw new NullReferenceException();

        return new PersonalVehicleRepository(Configuration, logger, httpClientFactory, tokenProvider);
    }
}
