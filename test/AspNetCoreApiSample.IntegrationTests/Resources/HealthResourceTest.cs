using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Withywoods.WebTesting;
using Xunit;

namespace Withywoods.AspNetCoreApiSample.IntegrationTests.Resources
{
    [Trait("Environment", "Localhost")]
    public class HealthResourceTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private const string ResourceEndpoint = "health";

        private readonly HttpClient _client;

        private readonly IHttpClientFactory _httpClientFactory;

        public HealthResourceTest(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();

            // it's another way to be able to create an HttpClient if we must go through an abstraction layer
            _httpClientFactory = new WebApplicationHttpClientFactory<Program>(factory);
        }

        [Fact]
        public async Task AspNetCoreApiSampleHealthResourceGet_ReturnsHttpOk()
        {
            // Arrange & Act
            var response = await _client.GetAsync($"/{ResourceEndpoint}");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            stringResponse.Should().Be("Healthy");
        }

        [Fact]
        public async Task AspNetCoreApiSampleHealthResourceGet_ByUsingHttpClientFactory_ReturnsHttpOk()
        {
            // Arrange
            var httpClient = _httpClientFactory.CreateClient();

            // Act
            var response = await httpClient.GetAsync($"/{ResourceEndpoint}");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            stringResponse.Should().Be("Healthy");
        }
    }
}
