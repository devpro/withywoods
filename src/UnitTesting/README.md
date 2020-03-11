# Withywoods Common Unit Testing

This library can be used by an .NET project.

- Astract class to be able to do unit tests on HTTP clients: `HttpRepositoryTestBase`, it provides in particular a working Mock of HttpClientFactory.

See a fully working example in [../../test/Net.Http.UnitTests/HttpRepositoryBaseTest.cs] file.

```csharp
using Withywoods.UnitTesting;

[Trait("Category", "UnitTests")]
public class HttpRepositoryBaseTest : HttpRepositoryTestBase
{
    [Fact]
    public async Task FakeHttpRepositoryFindAllAsync_ReturnSuccess()
    {
        // Arrange
        var fixture = new Fixture();
        var responseDto = fixture.Create<List<string>>();
        var httpResponseMessage = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(responseDto.ToJson())
        };
        var repository = BuildRepository(httpResponseMessage, HttpMethod.Get, "https://does.not.exist/v42/api/fakes");

        // Act
        var output = await repository.FindAllAsync();

        // Assert
        output.Should().NotBeNullOrEmpty();
        output.Count.Should().Be(responseDto.Count);
    }

    private FakeHttpRepository BuildRepository(HttpResponseMessage httpResponseMessage, HttpMethod httpMethod, string absoluteUri)
    {
        var logger = ServiceProvider.GetService<ILogger<FakeHttpRepository>>();
        var httpClientFactoryMock = BuildHttpClientFactory(httpResponseMessage, httpMethod, "FakeApi", absoluteUri);

        return new FakeHttpRepository(logger, httpClientFactoryMock.Object);
    }
}
```
