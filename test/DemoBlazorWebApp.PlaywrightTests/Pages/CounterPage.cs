using System.Threading.Tasks;
using Microsoft.Playwright;

namespace Withywoods.DemoBlazorWebApp.PlaywrightTests.Pages;

public class CounterPage(IPage page) : PageBase(page)
{
    protected override string WebPageTitle => "Counter";

    private ILocator TableRow => Page.Locator("table.table > li");

    private ILocator ClickMeButton => Page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Click Me" });

    private ILocator StatusMessage => Page.GetByRole(AriaRole.Status);

    public async Task ClickAndCheckAsync(int initialCount, int clickCount = 1)
    {
        await Assertions.Expect(StatusMessage).ToContainTextAsync($"Current count: {initialCount}");
        await ClickMeButton.ClickAsync(new LocatorClickOptions() { ClickCount = clickCount });
        await Assertions.Expect(StatusMessage).ToContainTextAsync($"Current count: {initialCount + clickCount}");
    }
}
