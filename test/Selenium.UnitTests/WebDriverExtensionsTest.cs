using System.Collections.ObjectModel;
using System.Drawing;
using FluentAssertions;
using Moq;
using OpenQA.Selenium;
using Xunit;

namespace Withywoods.Selenium.UnitTests
{
    [Trait("Category", "UnitTests")]
    public class WebDriverExtensionsTest
    {
        #region Inner classes

        /// <summary>
        /// Dumb class to have a concrete implementation of IWebElement.
        /// Only Displayed property is needed for the tests.
        /// </summary>
        public class DumbWebElement : IWebElement
        {
            public string TagName => throw new System.NotImplementedException();

            public string Text => throw new System.NotImplementedException();

            public bool Enabled => throw new System.NotImplementedException();

            public bool Selected => throw new System.NotImplementedException();

            public Point Location => throw new System.NotImplementedException();

            public Size Size => throw new System.NotImplementedException();

            public bool Displayed { get; set; }

            public void Clear()
            {
                throw new System.NotImplementedException();
            }

            public void Click()
            {
                throw new System.NotImplementedException();
            }

            public IWebElement FindElement(By by)
            {
                throw new System.NotImplementedException();
            }

            public ReadOnlyCollection<IWebElement> FindElements(By by)
            {
                throw new System.NotImplementedException();
            }

            public string GetAttribute(string attributeName)
            {
                throw new System.NotImplementedException();
            }

            public string GetCssValue(string propertyName)
            {
                throw new System.NotImplementedException();
            }

            public string GetProperty(string propertyName)
            {
                throw new System.NotImplementedException();
            }

            public void SendKeys(string text)
            {
                throw new System.NotImplementedException();
            }

            public void Submit()
            {
                throw new System.NotImplementedException();
            }
        }

        #endregion

        [Fact]
        public void WebDriverFindElementNotDisplayed_OnExistingThenDisappearingBehavior_ReturnsWebElement()
        {
            // Arrange
            var findBy = By.Id("toto");
            var webDriver = new Mock<IWebDriver>();
            webDriver.SetupSequence(x => x.FindElement(findBy))
                // the element is first displayed
                .Returns(new DumbWebElement { Displayed = true })
                .Returns(new DumbWebElement { Displayed = true })
                .Returns(new DumbWebElement { Displayed = true })
                // then it is hidden
                .Returns(new DumbWebElement { Displayed = false })
                .Returns(new DumbWebElement { Displayed = false });

            // Act
            var webElement = webDriver.Object.FindElementNotDisplayed(findBy, 120);

            // Assert
            webElement.Should().NotBeNull();
            webElement.Displayed.Should().BeFalse();
        }
    }
}
