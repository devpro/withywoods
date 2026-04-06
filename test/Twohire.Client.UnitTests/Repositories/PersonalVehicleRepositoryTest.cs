using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using AwesomeAssertions;
using Bogus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Withywoods.Twohire.Abstractions.Models;
using Withywoods.Twohire.Client.Repositories;
using Xunit;

namespace Withywoods.Twohire.Client.UnitTests.Repositories;

[Trait("Category", "UnitTests")]
public class PersonalVehicleRepositoryTest : RepositoryTestBase
{
    [Fact]
    public async Task PersonalVehicleRepositoryFindAllAsync_ReturnData()
    {
        // Arrange
        var responseModel = new Faker<ResponseModel<List<object>>>().Generate();
        var httpResponseMessage = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(JsonSerializer.Serialize(responseModel))
        };
        var repository = BuildRepository(httpResponseMessage, HttpMethod.Get, $"http://does.not.exist/v42/admin/api/personal/vehicle");

        // Act
        var output = await repository.FindAllAsync();

        // Assert
        output.Should().NotBeNull();
        output.Should().BeEquivalentTo(responseModel);
    }

    private PersonalVehicleRepository BuildRepository(HttpResponseMessage httpResponseMessage, HttpMethod httpMethod, string absoluteUri)
    {
        var logger = ServiceProvider.GetService<ILogger<PersonalVehicleRepository>>() ?? throw new NullReferenceException(nameof(PersonalVehicleRepository));
        var httpClientFactoryMock = BuildHttpClientFactory(httpResponseMessage, httpMethod, "Fake", absoluteUri);
        TokenProviderMock.Setup(x => x.Token).Returns("loveDotNet");

        return new PersonalVehicleRepository(Configuration, logger, httpClientFactoryMock.Object, TokenProviderMock.Object);
    }
}
