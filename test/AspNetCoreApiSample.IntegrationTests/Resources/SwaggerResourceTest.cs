using FluentAssertions;
using OpenQA.Selenium;
using Withywoods.Selenium;
using Withywoods.WebTesting;
using Xunit;

namespace Withywoods.AspNetCoreApiSample.IntegrationTests.Resources
{
    [Trait("Environment", "Localhost")]
    public class SwaggerResourceTest : SeleniumTestBase, IClassFixture<WebApplicationFactoryFixture<Program>>
    {
        private const string ResourceEndpoint = "swagger";

        private readonly string _webUrl = "https://localhost:5001";

        public SwaggerResourceTest(WebApplicationFactoryFixture<Program> factory)
            : base(new WebDriverOptions())
        {
            factory.HostUrl = _webUrl;
            factory.CreateDefaultClient();
        }

        [Fact]
        public void AspNetCoreApiSampleSwaggerResourceGet_ReturnsHttpOk()
        {
            try
            {
                // Arrange & Act
                WebDriver.Navigate().GoToUrl($"{_webUrl}/{ResourceEndpoint}");

                // Assert
                WebDriver.FindElement(By.ClassName("title"), 60);
                WebDriver.Title.Should().Be("Swagger UI");
                WebDriver.FindElement(By.ClassName("title")).Text.Should().Contain("My API");

                TakeScreenShot(nameof(AspNetCoreApiSampleSwaggerResourceGet_ReturnsHttpOk));
            }
            catch
            {
                TakeScreenShot(nameof(AspNetCoreApiSampleSwaggerResourceGet_ReturnsHttpOk));
                throw;
            }
        }
    }
}
