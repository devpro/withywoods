using System.Threading.Tasks;
using Withywoods.AspNetCore.Mvc.Testing;
using Withywoods.DemoBlazorWebApp.PlaywrightTests.Aspects;
using Withywoods.DemoBlazorWebApp.PlaywrightTests.Pages;
using Xunit;

namespace Withywoods.DemoBlazorWebApp.PlaywrightTests.Smoke;

public class NotFoundSmokeTest(KestrelWebAppFactory<Program> factory)
    : PlaywrightTestBase(factory)
{
    private readonly KestrelWebAppFactory<Program> _factory = factory;

    [Fact]
    [ScreenshotOnFailure]
    public async Task SmokeClickCounter_Succeeds()
    {
        var notFoundPage = new NotFoundPage(Page);
        await notFoundPage.NavigateToAsync($"{_factory.ServerAddress}/dummy");
    }
}
