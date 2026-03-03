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

    private ILocator CounterLink => Page.GetByRole(AriaRole.Link, new PageGetByRoleOptions { Name = "Count" });

    private ILocator WeatherForecastLink => Page.GetByRole(AriaRole.Link, new PageGetByRoleOptions { Name = "Weather" });

    // assertions

    public virtual async Task WaitForReadyAsync()
    {
        await Assertions.Expect(Page).ToHaveTitleAsync(WebPageTitle);
        await Assertions.Expect(PageHeader).ToBeVisibleAsync();
    }

    // actions

    public async Task NavigateToAsync(string baseAddress)
    {
        await Page.GotoAsync(baseAddress);
        await WaitForReadyAsync();
    }

    public async Task<HomePage> OpenHomeAsync()
    {
        await HomeLink.ClickAsync();
        var homePage = new HomePage(Page);
        await homePage.WaitForReadyAsync();
        return homePage;
    }

    public async Task<CounterPage> OpenCounterAsync()
    {
        await CounterLink.ClickAsync();
        var counterPage = new CounterPage(Page);
        await counterPage.WaitForReadyAsync();
        return counterPage;
    }

    public async Task<WeatherForecastPage> OpenWeatherForecastAsync()
    {
        await WeatherForecastLink.ClickAsync();
        var weatherForecastPage = new WeatherForecastPage(Page);
        await weatherForecastPage.WaitForReadyAsync();
        return weatherForecastPage;
    }
}
