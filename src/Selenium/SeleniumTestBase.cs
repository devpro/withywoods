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
    public class SeleniumTestBase : IDisposable
    {
        protected SeleniumTestBase(bool isHeadless = true, string chromeDriverEnvironmentVariableName = "ChromeWebDriver")
        {
            // if there is an issue with the CI run, it is a good advice to debug it locally without the headless option
            var chromeOptions = new ChromeOptions();
            if (isHeadless)
            {
                chromeOptions.AddArguments("--headless", "--ignore-certificate-errors");
            }
            else
            {
                chromeOptions.AddArguments("--ignore-certificate-errors");
            }

            // chrome driver is sensitive to chrome browser version, CI build should provide the path to driver
            // for Azure DevOps it's described here for example: https://github.com/actions/virtual-environments/blob/master/images/win/Windows2019-Readme.md
            var chromeDriverLocation = Environment.GetEnvironmentVariable(chromeDriverEnvironmentVariableName);
            if (string.IsNullOrEmpty(chromeDriverLocation))
            {
                chromeDriverLocation = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            }

            WebDriver = new ChromeDriver(chromeDriverLocation, chromeOptions);
        }

        protected RemoteWebDriver WebDriver { get; private set; }

        protected void TakeScreenShot(string methodName)
        {
            var screenshot = ((ITakesScreenshot)WebDriver).GetScreenshot();
            screenshot.SaveAsFile($"screenshot_{methodName}_{DateTime.UtcNow.ToString("yyyyMMdd_HHmmss")}.png");
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
