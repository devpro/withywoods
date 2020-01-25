using System;
using System.Net.Http;
using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using Withywoods.Selenium;
using Withywoods.WebTesting.TestHost;
using Xunit;

namespace Withywoods.AspNetCoreApiSample.IntegrationTests.Resources
{
    [Trait("Environment", "Localhost")]
    public class SwaggerResourceTest : IClassFixture<LocalServerFactory<Startup>>, IDisposable
    {
        private const string _ResourceEndpoint = "swagger";
        private const string _ChromeDriverEnvironmentVariableName = "ChromeWebDriver";

        private readonly HttpClient _httpClient;
        private readonly RemoteWebDriver _webDriver;
        private readonly LocalServerFactory<Startup> _server;

        public SwaggerResourceTest(LocalServerFactory<Startup> server)
        {
            _server = server;
            _httpClient = server.CreateClient();

            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("--headless");
            // chrome driver is sensitive to chrome browser version, CI build should provide the path to driver
            var chromeDriverLocation = string.IsNullOrEmpty(Environment.GetEnvironmentVariable(_ChromeDriverEnvironmentVariableName)) ?
                System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) :
                Environment.GetEnvironmentVariable(_ChromeDriverEnvironmentVariableName);
            _webDriver = new ChromeDriver(chromeDriverLocation, chromeOptions);
        }

        [Fact]
        public void AspNetCoreApiSampleSwaggerResourceGet_ReturnsHttpOk()
        {
            // Arrange & Act
            _webDriver.Navigate().GoToUrl($"{_server.RootUri}/{_ResourceEndpoint}");

            // Assert
            _webDriver.FindElement(By.ClassName("title"), 360);
            _webDriver.Title.Should().Be("Swagger UI");
            _webDriver.FindElementByClassName("title").Text.Should().Contain("My API");
        }

        public void Dispose()
        {
            _webDriver?.Dispose();
        }
    }
}
