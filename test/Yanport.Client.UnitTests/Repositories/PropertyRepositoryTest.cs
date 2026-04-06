using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using AwesomeAssertions;
using Bogus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Withywoods.Yanport.Abstractions.Models;
using Withywoods.Yanport.Abstractions.Repositories;
using Withywoods.Yanport.Client.Repositories;
using Xunit;

namespace Withywoods.Yanport.Client.UnitTests.Repositories;

[Trait("Category", nameof(UnitTests))]
public class PropertyRepositoryTest : RepositoryTestBase
{
    [Fact]
    public async Task PropertyRepositoryFindAllAsync_ReturnData()
    {
        // Arrange
        var faker = new Faker();
        var resultModel = new ResultModel
        {
            Total = 1,
            Hits =
            [
                new HitModel { Id = faker.Random.AlphaNumeric(8), HitType = faker.Random.AlphaNumeric(12) }
            ]
        };
        var httpResponseMessage = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(JsonSerializer.Serialize(resultModel))
        };
        var repository = BuildRepository(httpResponseMessage, HttpMethod.Get,
            $"http://doesnotexist.nop/properties?from=0&size=100&marketingTypes=SALE&active=true&published=true");

        // Act
        var output = await repository.FindAllAsync("?from=0&size=100&marketingTypes=SALE&active=true&published=true");

        // Assert
        output.Should().NotBeNullOrEmpty();
        output.Count.Should().Be(resultModel.Hits.Count);
    }

    private IPropertyRepository BuildRepository(HttpResponseMessage httpResponseMessage, HttpMethod httpMethod, string absoluteUri)
    {
        var logger = ServiceProvider.GetService<ILogger<PropertyRepository>>() ?? throw new NullReferenceException(nameof(PropertyRepository));
        var httpClientFactoryMock = BuildHttpClientFactory(httpResponseMessage, httpMethod, Configuration.HttpClientName, absoluteUri);

        return new PropertyRepository(Configuration, logger, httpClientFactoryMock.Object);
    }
}
