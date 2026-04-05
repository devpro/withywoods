using Microsoft.Playwright;

namespace Withywoods.DemoBlazorWebApp.PlaywrightTests.Pages;

public class HomePage(IPage page) : PageBase(page)
{
    protected override string WebPageTitle => "Home";
}
