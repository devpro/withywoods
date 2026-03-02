using System.Net;
using System.Threading.Tasks;
using AwesomeAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using VerifyXunit;
using Xunit;

namespace Withywoods.DemoWebApi.IntegrationTests.Resources;

[Trait("Category", "IntegrationTests")]
public class AuxiliaryResourceTest(WebApplicationFactory<Program> factory)
    : IClassFixture<WebApplicationFactory<Program>>
{
    [Trait("Mode", "Readonly")]
    [Theory]
    [InlineData("/health", HttpStatusCode.OK, "text/plain", "Healthy")]
    [InlineData("/scalar", HttpStatusCode.OK, "text/html")]
    [InlineData("/openapi/v1.json", HttpStatusCode.OK, "application/json; charset=utf-8")]
    public async Task AuxiliaryResource_Get_ReturnsExpectedResponse(string url,
        HttpStatusCode expectedStatus,
        string expectedContentType,
        string? expectedContent = "")
    {
        var client = factory.CreateClient();

        var response = await client.GetAsync(url, TestContext.Current.CancellationToken);
        response.StatusCode.Should().Be(expectedStatus);

        response.Content.Headers.ContentType?.ToString().Should().Be(expectedContentType);

        var result = await response.Content.ReadAsStringAsync(TestContext.Current.CancellationToken);
        await VerifyResult(result, expectedContentType, expectedContent, "../Snapshots", SnapshotNameFromUrl(url));
    }

    private static async Task VerifyResult(string result,
        string expectedContentType,
        string? expectedContent,
        string snapshotDirectory,
        string snapshotName)
    {
        if (!string.IsNullOrEmpty(expectedContent))
        {
            result.Should().Be(expectedContent);
            return;
        }

        if (expectedContentType.StartsWith("application/json"))
        {
            await Verifier.VerifyJson(result)
                .UseDirectory(snapshotDirectory)
                .UseFileName(snapshotName);
            return;
        }

        await Verifier.Verify(result)
            .UseDirectory(snapshotDirectory)
            .UseFileName(snapshotName);
    }

    private static string SnapshotNameFromUrl(string url)
    {
        return url
            .Trim('/')
            .Split('/', 2)[0];
    }
}
