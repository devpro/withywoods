using System;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;

namespace Withywoods.Selenium
{
    /// <summary>
    /// Selenium test base.
    /// Chrome is the only driver implemented for the moment.
    /// </summary>
    public abstract class SeleniumTestBase : IDisposable
    {
        protected SeleniumTestBase(WebDriverOptions webDriverOptions)
        {
            // if there is an issue with the CI run, it is a good advice to debug it locally without the headless option
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("--ignore-certificate-errors");
            chromeOptions.AddArgument($"--window-size={webDriverOptions.WindowWidth},{webDriverOptions.WindowHeight}");
            if (webDriverOptions.IsHeadless)
            {
                chromeOptions.AddArgument("--headless");
            }
            if (!string.IsNullOrEmpty(webDriverOptions.UserLanguages))
            {
                chromeOptions.AddUserProfilePreference("intl.accept_languages", webDriverOptions.UserLanguages);
            }

            // chrome driver is sensitive to chrome browser version, CI build should provide the path to driver
            // for Azure DevOps it's described here: https://github.com/actions/virtual-environments/blob/master/images/win/Windows2019-Readme.md
            var chromeDriverLocation = Environment.GetEnvironmentVariable(webDriverOptions.ChromeDriverEnvironmentVariableName);
            if (string.IsNullOrEmpty(chromeDriverLocation))
            {
                chromeDriverLocation = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            }

            WebDriver = new ChromeDriver(chromeDriverLocation, chromeOptions);
        }

        protected SeleniumTestBase(RemoteWebDriver webDriver)
        {
            WebDriver = webDriver;
        }

        protected SeleniumTestBase(SeleniumTestBase otherPage)
        {
            WebDriver = otherPage.WebDriver;
        }

        protected IWebDriver WebDriver { get; }

        protected virtual void OpenUrl(string rootUrl) => WebDriver.Navigate().GoToUrl($"{rootUrl}");

        protected virtual void TakeScreenShot(string methodName)
        {
            var screenshot = ((ITakesScreenshot)WebDriver).GetScreenshot();
            screenshot.SaveAsFile(GenerateScreenshotFilename(methodName));
        }

        protected virtual string GenerateScreenshotFilename(string methodName)
        {
            return $"screenshot_{methodName}_{DateTime.UtcNow:yyyyMMdd_HHmmss_fff}.png";
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                WebDriver?.Dispose();
            }
        }
    }
}
