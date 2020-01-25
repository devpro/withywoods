using System;
using OpenQA.Selenium.Chrome;
using Xunit;

namespace Withywoods.AspNetCoreApiSample.IntegrationTests.Resources
{
    public class TotoTest : IClassFixture<AspNetSiteServerFixture>
    {
        private Uri _rootUrl;

        public TotoTest(AspNetSiteServerFixture fixture)
        {
            _rootUrl = fixture.RootUri;
        }

        //[Fact]
        public void FullE2eTest()
        {
            var chromeOptions = new ChromeOptions();
            //chromeOptions.AddArguments("--headless");
            var chromeDriverLocation = string.IsNullOrEmpty(Environment.GetEnvironmentVariable("ChromeWebDriver")) ?
                System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) :
                Environment.GetEnvironmentVariable("ChromeWebDriver");
            var driver = new ChromeDriver(chromeDriverLocation, chromeOptions);
            driver.Navigate().GoToUrl(_rootUrl);
            Assert.Equal("TOSS", driver.Title);
        }
    }
}
