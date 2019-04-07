using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Withywoods.AspNetCoreApiSample.IntegrationTests.Resources
{
    [Trait("Environment", "Localhost")]
    public class HealthResourceTest : IClassFixture<WebApplicationFactory<Startup>>
    {
        private const string _ResourceEndpoint = "health";

        private readonly HttpClient _client;

        public HealthResourceTest(WebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task AspNetCoreApiSampleHealthResourceGet_ReturnsHttpOk()
        {
            // Arrange & Act
            var response = await _client.GetAsync($"/{_ResourceEndpoint}");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            stringResponse.Should().Be("Healthy");
        }
    }
}
