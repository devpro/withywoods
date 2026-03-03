using System;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Microsoft.Playwright.Xunit.v3;
using Withywoods.AspNetCore.Mvc.Testing;
using Withywoods.DemoBlazorWebApp.PlaywrightTests.Pages;
using Xunit;

namespace Withywoods.DemoBlazorWebApp.PlaywrightTests;

public abstract class PlaywrightTestBase(KestrelWebAppFactory<Program> factory)
    : PageTest(), IClassFixture<KestrelWebAppFactory<Program>>
{
    public override async ValueTask InitializeAsync()
    {
        await base.InitializeAsync();

        Page.SetDefaultTimeout(10_000);
        Page.SetDefaultNavigationTimeout(20_000);
    }

    public async Task TakeScreenshotAsync()
    {
        await Page.ScreenshotAsync(new PageScreenshotOptions
        {
            Path = $"failure_{DateTime.Now:yyyyMMdd-HHmmss}.png",
            FullPage = true
        });
    }

    protected async Task<HomePage> OpenHomePageAsync()
    {
        var homePage = new HomePage(Page);
        await homePage.NavigateToAsync(factory.ServerAddress);
        return homePage;
    }
}
