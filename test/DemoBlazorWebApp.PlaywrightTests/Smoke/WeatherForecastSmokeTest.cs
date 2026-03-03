using System.Threading.Tasks;
using Withywoods.AspNetCore.Mvc.Testing;
using Withywoods.DemoBlazorWebApp.PlaywrightTests.Aspects;
using Xunit;

namespace Withywoods.DemoBlazorWebApp.PlaywrightTests.Smoke;

public class WeatherForecastSmokeTest(KestrelWebAppFactory<Program> factory)
    : PlaywrightTestBase(factory)
{
    [Fact]
    [ScreenshotOnFailure]
    public async Task SmokeViewWeatherForecast_Succeeds()
    {
        var homePage = await OpenHomePageAsync();
        var weatherForecastPage = await homePage.OpenWeatherForecastAsync();
        await weatherForecastPage.HasTableRowAsync();
        await weatherForecastPage.OpenHomeAsync();
    }
}
