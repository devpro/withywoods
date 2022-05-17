using FluentAssertions;
using OpenQA.Selenium;
using Withywoods.Selenium;
using Withywoods.WebTesting;
using Xunit;

namespace Withywoods.RazorWebAppSample.IntegrationTests
{
    /// <summary>
    /// Smoke integration tests.
    /// </summary>
    /// <remarks>
    /// https://docs.microsoft.com/en-us/aspnet/core/test/integration-tests
    /// </remarks>
    public class SmokeSeleniumTest : SeleniumTestBase, IClassFixture<WebApplicationFactoryFixture<Program>>
    {
        private readonly string _webUrl = "https://localhost:7112";

        public SmokeSeleniumTest(WebApplicationFactoryFixture<Program> factory)
            : base(new WebDriverOptions())
        {
            factory.HostUrl = _webUrl;
            factory.CreateDefaultClient();
        }

        [Theory]
        [InlineData("/", "Welcome")]
        [InlineData("/Index", "Welcome")]
        [InlineData("/Privacy", "Privacy Policy")]
        public void Get_EndpointsReturnSuccessAndCorrectContentType(string url, string expected)
        {
            try
            {
                // Arrange & Act
                WebDriver.Navigate().GoToUrl($"{_webUrl}{url}");

                // Assert
                WebDriver.FindElement(By.TagName("h1"), 30);
                WebDriver.FindElement(By.TagName("h1")).Text.Should().Contain(expected);

                TakeScreenShot(nameof(Get_EndpointsReturnSuccessAndCorrectContentType));
            }
            catch
            {
                TakeScreenShot(nameof(Get_EndpointsReturnSuccessAndCorrectContentType));
                throw;
            }
        }
    }
}
