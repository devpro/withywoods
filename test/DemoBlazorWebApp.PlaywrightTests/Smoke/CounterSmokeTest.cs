using System.Threading.Tasks;
using Withywoods.AspNetCore.Mvc.Testing;
using Withywoods.DemoBlazorWebApp.PlaywrightTests.Aspects;
using Xunit;

namespace Withywoods.DemoBlazorWebApp.PlaywrightTests.Smoke;

public class CounterSmokeTest(KestrelWebAppFactory<Program> factory)
    : PlaywrightTestBase(factory)
{
    [Fact]
    [ScreenshotOnFailure]
    public async Task SmokeClickCounter_Succeeds()
    {
        var homePage = await OpenHomePageAsync();
        var counterPage = await homePage.OpenCounterAsync();
        await counterPage.ClickAndCheckAsync(0, 1);
        await counterPage.ClickAndCheckAsync(1, 3);
        await counterPage.OpenHomeAsync();
    }
}
