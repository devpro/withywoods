using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using AwesomeAssertions;
using Bogus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Withywoods.Twohire.Abstractions.Models;
using Withywoods.Twohire.Client.Dto;
using Withywoods.Twohire.Client.Repositories;
using Xunit;

namespace Withywoods.Twohire.Client.UnitTests.Repositories;

[Trait("Category", "UnitTests")]
public class TokenRepositoryTest : RepositoryTestBase
{
    [Fact]
    public async Task TokenRepositoryCreateAsync_ReturnToken()
    {
        // Arrange
        var faker = new Faker();
        var responseDto = new ResponseModel<TokenDataDto>
        {
            Status = true,
            Data = new TokenDataDto
            {
                Token = new TokenDto
                {
                    Id = faker.Random.Int(),
                    Code = faker.Random.AlphaNumeric(32),
                    Expire = faker.Random.Long(1000, 9999),
                    ClientType = faker.Random.Int(),
                    UserId = faker.Random.Int(),
                    CreatedAt = faker.Date.Past(),
                    UpdatedAt = faker.Date.Recent(),
                    Unlimited = faker.Random.Bool(),
                    User = new UserDto
                    {
                    }
                }
            }
        };
        var httpResponseMessage = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(JsonSerializer.Serialize(responseDto))
        };
        var repository = BuildRepository(httpResponseMessage, HttpMethod.Post, $"http://does.not.exist/v42/admin/login");

        // Act
        var output = await repository.CreateAsync();

        // Assert
        output.Should().NotBeNull();
        output.Value.Should().Be(responseDto.Data.Token.Code);
        output.ExpiredDate.Should().Be(responseDto.Data.Token.CreatedAt.Add(new TimeSpan(responseDto.Data.Token.Expire)));
    }

    private TokenRepository BuildRepository(HttpResponseMessage httpResponseMessage, HttpMethod httpMethod, string absoluteUri)
    {
        var logger = ServiceProvider.GetService<ILogger<TokenRepository>>() ?? throw new NullReferenceException(nameof(TokenRepository));
        var httpClientFactoryMock = BuildHttpClientFactory(httpResponseMessage, httpMethod, "Fake", absoluteUri);

        return new TokenRepository(Configuration, logger, httpClientFactoryMock.Object);
    }
}
