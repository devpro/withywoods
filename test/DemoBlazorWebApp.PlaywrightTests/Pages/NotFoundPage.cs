using Microsoft.Playwright;

namespace Withywoods.DemoBlazorWebApp.PlaywrightTests.Pages;

public class NotFoundPage(IPage page) : PageBase(page)
{
    protected override string WebPageTitle => "Not Found";
}
