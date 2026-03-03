using System.Threading.Tasks;
using AwesomeAssertions;
using Microsoft.Playwright;

namespace Withywoods.DemoBlazorWebApp.PlaywrightTests.Pages;

public class WeatherForecastPage(IPage page) : PageBase(page)
{
    protected override string WebPageTitle => "Weather";

    private ILocator LoadingMessage => Page.GetByTestId("loading-message");

    private ILocator TableRow => Page.Locator("table tbody tr td").First;

    public override async Task WaitForReadyAsync()
    {
        await Assertions.Expect(LoadingMessage).ToBeHiddenAsync();
        await base.WaitForReadyAsync();
    }

    public async Task HasTableRowAsync()
    {
        await Assertions.Expect(TableRow).ToBeVisibleAsync();
    }
}
