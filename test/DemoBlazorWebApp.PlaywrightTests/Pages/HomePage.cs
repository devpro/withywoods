using System.Threading.Tasks;
using Microsoft.Playwright;

namespace Withywoods.DemoBlazorWebApp.PlaywrightTests.Pages;

public class HomePage(IPage page) : PageBase(page)
{
    protected override string WebPageTitle => "Home";

    public async Task NavigateToAsync(string baseAddress)
    {
        await Page.GotoAsync(baseAddress);
        await WaitForReadyAsync();
    }
}
