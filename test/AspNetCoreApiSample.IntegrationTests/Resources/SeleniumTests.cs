using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using Xunit;

namespace Withywoods.AspNetCoreApiSample.IntegrationTests.Resources
{
    public class SeleniumTests : IClassFixture<SeleniumServerFactory<Startup>>
    {
        private readonly SeleniumServerFactory<Startup> _server;
        private readonly IWebDriver _browser;

        public SeleniumTests(SeleniumServerFactory<Startup> server)
        {
            _server = server;
            //server.CreateClient();
            var opts = new ChromeOptions();
            //opts.AddArgument("--headless"); // Optional, comment this out if you want to SEE the browser window
            opts.AddArgument("no-sandbox");
            _browser = new RemoteWebDriver(opts);
        }

        //[Fact]
        public void BannerTextEqualsWelcome()
        {
            _browser.Navigate().GoToUrl(_server.RootUri);
            var bannerText = _browser.FindElement(By.ClassName("title"));
            Assert.Equal("Welcome", bannerText.Text);
            bannerText.Text.Should().Be("toto");
        }
    }
}
