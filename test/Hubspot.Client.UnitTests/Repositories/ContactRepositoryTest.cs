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
public class ContactRepositoryTest()
    : RepositoryTestBase<ContactRepository>(
        (config, logger, httpClientFactory, tokenProvider) => new ContactRepository(config, logger, httpClientFactory, tokenProvider))
{
    [Fact]
    public async Task ContactRepositoryFindAllAsync_ReturnData()
    {
        // Arrange
        var resultModel = new Faker<ContactResultModel>()
            .Generate();

        var httpResponseMessage = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(JsonSerializer.Serialize(resultModel))
        };
        Configuration.UseOAuth = true;
        TokenProviderMock.Setup(x => x.Token).Returns("loveDotNet");
        var repository = BuildRepository(httpResponseMessage, HttpMethod.Get, $"{Configuration.BaseUrl}/contacts/v1/lists/all/contacts/all");

        // Act
        var output = await repository.FindAllAsync();

        // Assert
        output.Should().NotBeNull();
    }
}
