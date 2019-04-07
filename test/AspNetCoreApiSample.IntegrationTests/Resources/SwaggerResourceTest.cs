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

        private readonly HttpClient _httpClient;
        private readonly RemoteWebDriver _webDriver;
        private readonly LocalServerFactory<Startup> _server;

        public SwaggerResourceTest(LocalServerFactory<Startup> server)
        {
            _server = server;
            _httpClient = server.CreateClient();

            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("--headless");
            var executingAssembly = System.Reflection.Assembly.GetExecutingAssembly();
            var currentLocation = System.IO.Path.GetDirectoryName(executingAssembly.Location);
            _webDriver = new ChromeDriver(currentLocation, chromeOptions);
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
            if (_webDriver != null)
            {
                _webDriver.Dispose();
            }
        }
    }
}
