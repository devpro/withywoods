using System;
using System.Net.Http;
using System.Threading.Tasks;
using AwesomeAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Withywoods.Twohire.Abstractions.Repositories;
using Withywoods.Twohire.Client.Repositories;
using Xunit;

namespace Withywoods.Twohire.Client.IntegrationTests.Sandbox;

public class TokenRepositorySandboxIntegrationTest : RepositoryIntegrationTestBase
{
    // TODO: enable it
    // [Fact]
    public async Task TokenRepositorySandboxCreateAsync_ReturnToken()
    {
        // Arrange
        var repository = BuildRepository();

        // Act
        var output = await repository.CreateAsync();

        // Assert
        output.Should().NotBeNull();
        output.Value.Should().NotBeNullOrEmpty();
        output.ExpiredDate.Should().BeAfter(DateTime.Now);
    }

    private TokenRepository BuildRepository()
    {
        var logger = ServiceProvider.GetService<ILogger<TokenRepository>>() ?? throw new NullReferenceException();
        var httpClientFactory = ServiceProvider.GetService<IHttpClientFactory>() ?? throw new NullReferenceException();

        return new TokenRepository(Configuration, logger, httpClientFactory);
    }
}
