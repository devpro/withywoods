using System;
using System.IO;
using FluentAssertions;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using OpenQA.Selenium.Chrome;
using Xunit;

namespace Withywoods.AspNetCoreApiSample.IntegrationTests.Resources
{
    public class SelenuimSampleTests : IDisposable
    {
        private const string TestLocalHostUrl = "https://localhost:5001";

        private readonly IWebHost _webHost;

        public SelenuimSampleTests()
        {
            _webHost = WebHost.CreateDefaultBuilder()
                .UseStartup<Startup>()
                .Build();

            _webHost.RunAsync();
        }

        public void Dispose()
        {
            _webHost?.StopAsync();
        }

        //[Fact]
        public void TestWithSelenium()
        {
            var currentDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            using (var driver = new ChromeDriver(currentDirectory))
            {
                driver.Navigate().GoToUrl(TestLocalHostUrl);
                var webElement = driver.FindElementByClassName("title");
                webElement.Should().Be("toto");
            }
        }
    }
}
