using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Withywoods.Selenium
{
    /// <summary>
    /// Extensions for Selenium <see cref="IWebDriver"/>.
    /// </summary>
    public static class WebDriverExtensions
    {
        /// <summary>
        /// Find an element by a selector and using a driver wait.
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="by"></param>
        /// <param name="timeoutInSeconds"></param>
        /// <returns></returns>
        public static IWebElement FindElement(this IWebDriver driver, By by, int timeoutInSeconds)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
            return wait.Until(drv => drv.FindElement(by));
        }

        /// <summary>
        /// Find an element that must be present but not displayed by using a selector and a driver wait.
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="by"></param>
        /// <param name="timeoutInSeconds"></param>
        /// <returns></returns>
        public static IWebElement FindElementNotDisplayed(this IWebDriver driver, By by, int timeoutInSeconds)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
            return wait.Until(drv =>
                (drv.FindElement(by) != null && !drv.FindElement(by).Displayed)
                ? drv.FindElement(by)
                : null);
        }

        /// <summary>
        /// Find an element that must be present and whose text is not empty.
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="by"></param>
        /// <param name="timeoutInSeconds"></param>
        /// <returns></returns>
        public static IWebElement FindElementWithNoEmptyText(this IWebDriver driver, By by, int timeoutInSeconds)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
            return wait.Until(drv =>
                (drv.FindElement(by) != null && drv.FindElement(by).Displayed && !string.IsNullOrEmpty(drv.FindElement(by).Text))
                ? drv.FindElement(by)
                : null);
        }

        /// <summary>
        /// Find an element that must be present and whose text is the one we expect.
        /// Can be useful on async changes in a page.
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="by"></param>
        /// <param name="timeoutInSeconds"></param>
        /// <returns></returns>
        public static IWebElement FindElementWithExpectedText(this IWebDriver driver, By by, string expected, int timeoutInSeconds)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
            return wait.Until(drv =>
                (drv.FindElement(by) != null && drv.FindElement(by).Displayed && !string.IsNullOrEmpty(drv.FindElement(by).Text) && drv.FindElement(by).Text == expected)
                ? drv.FindElement(by)
                : null);
        }
    }
}
