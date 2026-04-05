using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoFixture;
using Devpro.Yanport.Abstractions.Models;
using Devpro.Yanport.Abstractions.Repositories;
using Devpro.Yanport.Client.Repositories;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Withywoods.Serialization.Json;
using Xunit;

namespace Devpro.Yanport.Client.UnitTests.Repositories
{
    [Trait("Category", "UnitTests")]
    public class PropertyRepositoryTest : RepositoryTestBase
    {
        [Fact]
        public async Task PropertyRepositoryFindAllAsync_ReturnData()
        {
            // Arrange
            var fixture = new Fixture();
            var responseDto = fixture.Create<ResultModel>();
            var httpResponseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(responseDto.ToJson())
            };
            var repository = BuildRepository(httpResponseMessage, HttpMethod.Get, $"http://doesnotexist.nop/properties?from=0&size=100&marketingTypes=SALE&active=true&published=true");

            // Act
            var output = await repository.FindAllAsync("?from=0&size=100&marketingTypes=SALE&active=true&published=true");

            // Assert
            output.Should().NotBeNullOrEmpty();
            output.Count.Should().Be(responseDto.Hits.Count);
        }

        private IPropertyRepository BuildRepository(HttpResponseMessage httpResponseMessage, HttpMethod httpMethod, string absoluteUri)
        {
            var logger = ServiceProvider.GetService<ILogger<PropertyRepository>>();
            var httpClientFactoryMock = BuildHttpClientFactory(httpResponseMessage, httpMethod, Configuration.HttpClientName, absoluteUri);

            return new PropertyRepository(Configuration, logger, httpClientFactoryMock.Object);
        }
    }
}
