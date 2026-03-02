using System.Net;
using System.Threading.Tasks;
using AwesomeAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Withywoods.DemoWebApi.IntegrationTests.Resources;

[Trait("Category", "IntegrationTests")]
public class HealthCheckResourceTest(WebApplicationFactory<Program> factory)
    : IClassFixture<WebApplicationFactory<Program>>
{
    [Fact]
    [Trait("Mode", "Readonly")]
    public async Task HealthCheckResource_Get_ReturnsOk()
    {
        var client = factory.CreateClient();
        var response = await client.GetAsync("/health", TestContext.Current.CancellationToken);
        var result = await response.Content.ReadAsStringAsync(TestContext.Current.CancellationToken);
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        result.Should().NotBeNull();
        result.Should().Be("Healthy");
    }
}
