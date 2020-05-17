using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Withywoods.Selenium
{
    public static class WebElementExtensions
    {
        /// <summary>
        /// Fill text on a web element (clear + send keys).
        /// </summary>
        /// <param name="webElement"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static IWebElement FillText(this IWebElement webElement, string text)
        {
            webElement.Clear();
            webElement.SendKeys(text);
            return webElement;
        }

        /// <summary>
        /// Select an option in a selection web element from its text.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static IWebElement SelectByText(this IWebElement element, string text)
        {
            var select = new SelectElement(element);
            select.SelectByText(text);
            return element;
        }

        /// <summary>
        /// Select an option in a selection web element from its value.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IWebElement SelectByValue(this IWebElement element, string value)
        {
            var select = new SelectElement(element);
            select.SelectByValue(value);
            return element;
        }
    }
}
