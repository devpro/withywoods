using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using AwesomeAssertions;
using Bogus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Withywoods.Net.Http.UnitTests.Fakes;
using Xunit;

namespace Withywoods.Net.Http.UnitTests;

[Trait("Category", "UnitTests")]
public class HttpRepositoryBaseTest : HttpRepositoryTestBase
{
    private readonly Faker _faker = new Faker();

    // GetAsync tests

    [Fact]
    public async Task FakeHttpRepositoryFindAllAsync_ReturnSuccess()
    {
        // Arrange
        var responseDto = Enumerable.Range(1, 5).Select(_ => _faker.Random.AlphaNumeric(8)).ToList();
        var httpResponseMessage = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(JsonSerializer.Serialize(responseDto))
        };
        var repository = BuildRepository(httpResponseMessage, HttpMethod.Get, "https://does.not.exist/v42/api/fakes");

        // Act
        var output = await repository.FindAllAsync<string>();

        // Assert
        output.Should().NotBeNullOrEmpty();
        output.Count.Should().Be(responseDto.Count);
    }

    [Fact]
    public async Task FakeHttpRepositoryFindAllAsync_WhenErrorStatus_ThrowsException()
    {
        // Arrange
        var httpResponseMessage = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.BadRequest,
            ReasonPhrase = "No particular reason",
            Content = new StringContent("Dude where is my car?")
        };
        var repository = BuildRepository(httpResponseMessage, HttpMethod.Get, "https://does.not.exist/v42/api/fakes");

        // Act
        var exc = await Assert.ThrowsAsync<ConnectivityException>(async () => await repository.FindAllAsync<string>());

        // Assert
        exc.Should().NotBeNull();
        exc.Message.Should().Be("The response status \"BadRequest\" is not a success (reason=\"No particular reason\")");
    }

    [Fact]
    public async Task FakeHttpRepositoryFindAllAsync_WhenEmptyResult_ThrowsException()
    {
        // Arrange
        var httpResponseMessage = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(string.Empty)
        };
        var repository = BuildRepository(httpResponseMessage, HttpMethod.Get, "https://does.not.exist/v42/api/fakes");

        // Act
        var exc = await Assert.ThrowsAsync<ConnectivityException>(async () => await repository.FindAllAsync<string>());

        // Assert
        exc.Should().NotBeNull();
        exc.Message.Should().Be("Empty response received while calling https://does.not.exist/v42/api/fakes");
    }

    [Fact]
    public async Task FakeHttpRepositoryFindAllAsync_WhenInvalidData_ThrowsException()
    {
        // Arrange
        var httpResponseMessage = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(JsonSerializer.Serialize(42))
        };
        var repository = BuildRepository(httpResponseMessage, HttpMethod.Get, "https://does.not.exist/v42/api/fakes");

        // Act
        var exc = await Assert.ThrowsAsync<ConnectivityException>(async () => await repository.FindAllAsync<string>());

        // Assert
        exc.Should().NotBeNull();
        exc.Message.Should().Contain("Invalid data received when calling \"https://does.not.exist/v42/api/fakes\": The JSON value could not be converted to System.Collections.Generic.List`1[System.String]. Path: $ | LineNumber: 0 | BytePositionInLine: 2.");
    }

    [Fact]
    public async Task FakeHttpRepositoryFindAllAsync_WhenInvalidMock_ThrowsException()
    {
        // Arrange
        var httpResponseMessage = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(JsonSerializer.Serialize(42))
        };
        var repository = BuildRepository(httpResponseMessage, HttpMethod.Get, "https://does.not.exist/v13/api/fakes");

        // Act
        var exc = await Assert.ThrowsAsync<NotImplementedException>(async () => await repository.FindAllAsync<string>());

        // Assert
        exc.Should().NotBeNull();
        exc.Message.Should().Contain("This code shouldn't be executed, the GET call to https://does.not.exist/v42/api/fakes must be mocked.");
    }

    [Fact]
    public async Task FakeHttpRepositoryFindAllAsync_WhenUnnamedClient_ReturnSuccess()
    {
        // Arrange
        var responseDto = Enumerable.Range(1, 5).Select(_ => _faker.Random.AlphaNumeric(8)).ToList();
        var httpResponseMessage = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(JsonSerializer.Serialize(responseDto))
        };
        var repository = BuildUnnamedClientRepository(httpResponseMessage, HttpMethod.Get, "https://still.not.here/api/fakes");

        // Act
        var output = await repository.FindAllAsync();

        // Assert
        output.Should().NotBeNullOrEmpty();
        output.Count.Should().Be(responseDto.Count);
    }

    // PostAsync tests

    [Fact]
    public async Task FakeHttpRepositoryCreateAsync_ReturnSuccess()
    {
        // Arrange
        var dto = new Faker<FakeDto>()
            .Rules((f, o) => { o.Id = f.Random.AlphaNumeric(8); o.Name = f.Random.AlphaNumeric(8); })
            .Generate();
        var httpResponseMessage = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(JsonSerializer.Serialize(dto))
        };
        var repository = BuildRepository(httpResponseMessage, HttpMethod.Post, "https://does.not.exist/v42/api/fakes");

        // Act
        var output = await repository.CreateAsync(dto);

        // Assert
        output.Should().NotBeNull();
        output.Should().BeEquivalentTo(dto);
    }

    // PutAsync tests

    [Fact]
    public async Task FakeHttpRepositoryUpdateAsync_ReturnSuccess()
    {
        // Arrange
        var dto = new Faker<FakeDto>()
            .Rules((f, o) => { o.Id = f.Random.AlphaNumeric(8); o.Name = f.Random.AlphaNumeric(8); })
            .Generate();
        var httpResponseMessage = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(JsonSerializer.Serialize(dto))
        };
        var repository = BuildRepository(httpResponseMessage, HttpMethod.Put, "https://does.not.exist/v42/api/fakes/123456");

        // Act
        await repository.UpdateAsync("123456", dto);
    }

    [Fact]
    public async Task FakeHttpRepositoryUpdateAsync_WithNoContent_ReturnSuccess()
    {
        // Arrange
        var dto = new Faker<FakeDto>()
            .Rules((f, o) => { o.Id = f.Random.AlphaNumeric(8); o.Name = f.Random.AlphaNumeric(8); })
            .Generate();
        var httpResponseMessage = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK
        };
        var repository = BuildRepository(httpResponseMessage, HttpMethod.Put, "https://does.not.exist/v42/api/fakes/123456");

        // Act
        await repository.UpdateAsync("123456", dto);
    }

    // DeleteAsync tests

    [Fact]
    public async Task FakeHttpRepositoryDeleteAsync_ReturnSuccess()
    {
        // Arrange
        var httpResponseMessage = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK
        };
        var repository = BuildRepository(httpResponseMessage, HttpMethod.Delete, "https://does.not.exist/v42/api/fakes/123456");

        // Act
        await repository.DeleteAsync("123456");
    }

    [Fact]
    public async Task FakeHttpRepositoryDeleteAsync_WithNotFound_ReturnSucess()
    {
        // Arrange
        var httpResponseMessage = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.NotFound
        };
        var repository = BuildRepository(httpResponseMessage, HttpMethod.Delete, "https://does.not.exist/v42/api/fakes/123456");

        // Act
        await repository.DeleteAsync("123456");
    }

    // Private methods tests

    private FakeHttpRepository BuildRepository(HttpResponseMessage httpResponseMessage, HttpMethod httpMethod, string absoluteUri)
    {
        var logger = ServiceProvider.GetService<ILogger<FakeHttpRepository>>() ?? throw new NullReferenceException(nameof(FakeHttpRepository));
        var httpClientFactoryMock = BuildHttpClientFactory(httpResponseMessage, httpMethod, "FakeApi", absoluteUri);

        return new FakeHttpRepository(logger, httpClientFactoryMock.Object);
    }

    private FakeHttpUnnamedClientRepository BuildUnnamedClientRepository(HttpResponseMessage httpResponseMessage, HttpMethod httpMethod, string absoluteUri)
    {
        return new FakeHttpUnnamedClientRepository(
            ServiceProvider.GetService<ILogger<FakeHttpRepository>>() ?? throw new NullReferenceException(nameof(FakeHttpRepository)),
            BuildHttpClientFactory(httpResponseMessage, httpMethod, "", absoluteUri).Object);
    }
}
