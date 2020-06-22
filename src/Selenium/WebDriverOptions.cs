namespace Withywoods.Selenium
{
    public class WebDriverOptions
    {
        public bool IsHeadless { get; set; } = true;

        public string ChromeDriverEnvironmentVariableName { get; set; } = "ChromeWebDriver";

        public int WindowWidth { get; set; } = 1600;

        public int WindowHeight { get; set; } = 900;
    }
}
