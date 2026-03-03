using Microsoft.Playwright;

namespace Withywoods.DemoBlazorWebApp.PlaywrightTests.Pages;

public class WeatherForecastPage(IPage page) : PageBase(page)
{
    protected override string WebPageTitle => "Home";
}
