using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Playwright;

namespace Withywoods.DemoBlazorWebApp.PlaywrightTests.Pages;

public abstract class PageBase(IPage page)
{
    // base

    protected IPage Page { get; } = page;

    protected abstract string WebPageTitle { get; }

    // locators

    private ILocator PageHeader => Page.Locator("h1");

    private ILocator HomeLink => Page.GetByRole(AriaRole.Link, new PageGetByRoleOptions { Name = "Home" });

    private ILocator WeatherForecastLink => Page.GetByRole(AriaRole.Link, new PageGetByRoleOptions { Name = "Weather" });

    // assertions

    public virtual async Task WaitForReadyAsync()
    {
        await Assertions.Expect(Page).ToHaveTitleAsync(WebPageTitle);
        await Assertions.Expect(PageHeader).ToBeVisibleAsync();
    }

    public virtual async Task VerifyPageHeaderAsync(string header)
    {
        await Assertions.Expect(PageHeader).ToHaveTextAsync(header);
    }

    public virtual async Task VerifyPageHeaderAsync(Regex header)
    {
        await Assertions.Expect(PageHeader).ToHaveTextAsync(header);
    }

    // actions

    public async Task<HomePage> OpenHomeAsync()
    {
        await HomeLink.ClickAsync();
        var homePage = new HomePage(Page);
        await homePage.WaitForReadyAsync();
        return homePage;
    }

    public async Task<WeatherForecastPage> OpenWeatherForecastAsync()
    {
        await WeatherForecastLink.ClickAsync();
        var todoPage = new WeatherForecastPage(Page);
        await todoPage.WaitForReadyAsync();
        return todoPage;
    }
}
