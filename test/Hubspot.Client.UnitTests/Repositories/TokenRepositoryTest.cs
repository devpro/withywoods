using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using AwesomeAssertions;
using Bogus;
using Withywoods.Hubspot.Abstractions.Models;
using Withywoods.Hubspot.Client.Repositories;
using Xunit;

namespace Withywoods.Hubspot.Client.UnitTests.Repositories;

[Trait("Category", "UnitTests")]
public class TokenRepositoryTest()
    : RepositoryTestBase<TokenRepository>(
        (config, logger, httpClientFactory, tokenProvider) => new TokenRepository(config, logger, httpClientFactory))
{
    [Fact]
    public async Task TokenRepositoryCreateAsync_ReturnToken()
    {
        // Arrange
        var resultModel = new Faker<TokenModel>()
            .Generate();
        resultModel.ExpiresIn = 1200;
        var httpResponseMessage = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(JsonSerializer.Serialize(resultModel))
        };
        Configuration.UseOAuth = true;
        var repository = BuildRepository(httpResponseMessage, HttpMethod.Post, $"{Configuration.BaseUrl}/oauth/v1/token");

        // Act
        var output = await repository.CreateAsync("order66");

        // Assert
        output.Should().NotBeNull();
        output.AccessToken.Should().Be(resultModel.AccessToken);
        output.RefreshToken.Should().Be(resultModel.RefreshToken);
        output.ExpiresIn.Should().Be((long)resultModel.ExpiresIn);
        output.ExpiredAt.Should().Be(output.CreatedAt.AddSeconds(resultModel.ExpiresIn));
    }
}
