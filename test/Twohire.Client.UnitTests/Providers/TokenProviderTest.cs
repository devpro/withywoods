using System;
using AwesomeAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using Withywoods.Twohire.Abstractions.Models;
using Withywoods.Twohire.Abstractions.Providers;
using Withywoods.Twohire.Abstractions.Repositories;
using Withywoods.Twohire.Client.Providers;
using Xunit;

namespace Withywoods.Twohire.Client.UnitTests.Providers;

[Trait("Category", "UnitTests")]
public class TokenProviderTest
{
    [Fact]
    public void TokenProviderGet_ReturnToken()
    {
        // Arrange
        var tokenModel = new TokenModel { ExpiredDate = DateTime.UtcNow.AddDays(1), Value = "MyToken42" };
        var provider = BuildProvider(tokenModel);

        // Act
        var output = provider.Token;

        // Assert
        output.Should().Be(tokenModel.Value);
    }

    private static ITokenProvider BuildProvider(TokenModel tokenModel)
    {
        var services = new ServiceCollection()
            .AddLogging();
        var serviceProvider = services.BuildServiceProvider();

        var logger = serviceProvider.GetService<ILogger<TokenProvider>>() ??  throw new NullReferenceException();

        var repositoryMock = new Mock<ITokenRepository>();
        repositoryMock.Setup(x => x.CreateAsync())
            .ReturnsAsync(tokenModel);

        return new TokenProvider(logger, repositoryMock.Object);
    }
}
