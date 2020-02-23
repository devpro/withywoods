# Withywoods Selenium Library

This library can be used by an .NET project.

## Features

- Extensions
  - Look & wait for an element

```csharp
using Withywoods.Selenium;

_webDriver.FindElement(By.ClassName("title"), 360); // will wait 360 seconds to the element to show up
```

- SeleniumTestBase class

```csharp
using Withywoods.Selenium;

public class SomeWebUserInterfaceTest : SeleniumTestBase
{
    [Fact]
    public void AspNetCoreApiSampleSwaggerResourceGet_ReturnsHttpOk()
    {
        _server.RootUri.Should().Be("https://localhost:5001");

        try
        {
            // Arrange & Act
            WebDriver.Navigate().GoToUrl($"{_server.RootUri}/{_ResourceEndpoint}");

            // Assert
            WebDriver.FindElement(By.ClassName("title"), 60);
            WebDriver.Title.Should().Be("Swagger UI");
            WebDriver.FindElementByClassName("title").Text.Should().Contain("My API");
        }
        catch
        {
            TakeScreenShot(nameof(AspNetCoreApiSampleSwaggerResourceGet_ReturnsHttpOk));
            throw;
        }
    }
}
```
