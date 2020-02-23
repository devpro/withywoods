using FluentAssertions;
using OpenQA.Selenium.Chrome;
using Xunit;

namespace Withywoods.Selenium.UnitTests
{
    [Trait("Category", "UnitTests")]
    public class FakeSeleniumTest : SeleniumTestBase
    {
        public FakeSeleniumTest()
            : base(true, "variable_does_not_exist")
        {
        }

        [Fact]
        public void Check()
        {
            // Arrange and Act are done in the constructor

            // Assert
            WebDriver.Should().BeOfType(typeof(ChromeDriver));
            WebDriver.Navigate().GoToUrl("https://dotnet.microsoft.com/");
            WebDriver.Title.Should().NotBeNullOrEmpty();
        }
    }
}
