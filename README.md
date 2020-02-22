# Devpro Withywoods - Shared .NET libraries

[![Build Status](https://dev.azure.com/devprofr/open-source/_apis/build/status/libraries/withywoods-ci?branchName=master)](https://dev.azure.com/devprofr/open-source/_build/latest?definitionId=31&branchName=master)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=withywoods&metric=alert_status)](https://sonarcloud.io/dashboard?id=withywoods)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=withywoods&metric=coverage)](https://sonarcloud.io/dashboard?id=withywoods)

Whithywoods is a set of small independant .NET libraries (Standard/Core). The goal is to do better with less code and capitalize on best practices (#KISS #DRY).

All libraries are available on [nuget.org](https://www.nuget.org/). Feel free to report any issue or ask for a change. You can also contribute with Pull Requests on GitHub!

NB: The name _Whithywoods_ comes from [Robin Hobb](https://twitter.com/robinhobb)'s incredible writing.

## Getting started

### Common

#### Configuration

[![Version](https://img.shields.io/nuget/v/Withywoods.Configuration.svg)](https://www.nuget.org/packages/Withywoods.Configuration/)
[![Downloads](https://img.shields.io/nuget/dt/Withywoods.Configuration.svg)](https://www.nuget.org/packages/Withywoods.Configuration/)

New extension method to access configuration: `configuration.TryGetSection()`.

[More information](./docs/Common.md)

#### Serialization

[![Version](https://img.shields.io/nuget/v/Withywoods.Serialization.svg)](https://www.nuget.org/packages/Withywoods.Serialization/)
[![Downloads](https://img.shields.io/nuget/dt/Withywoods.Serialization.svg)](https://www.nuget.org/packages/Withywoods.Serialization/)

Small extensions methods: `myObject.ToJson()` and `myString.FromJson()`.

[More information](./docs/Common.md)

### Data Access Layer (DAL)

#### MongoDB DAL

[![Version](https://img.shields.io/nuget/v/Withywoods.Dal.MongoDb.svg)](https://www.nuget.org/packages/Withywoods.Dal.MongoDb/)
[![Downloads](https://img.shields.io/nuget/dt/Withywoods.Dal.MongoDb.svg)](https://www.nuget.org/packages/Withywoods.Dal.MongoDb/)

No need to dig into MongoDB driver documentation and experimentation, just use the provided `RepositoryBase`!

Configuration interface and dependency injection will help through defining only what is required and you have the choice to configure it the way you want (no forced section names in your configuration file).

[More information](./docs/DalMongoDB.md).

### Web

#### Selenium

[![Version](https://img.shields.io/nuget/v/Withywoods.Selenium.svg)](https://www.nuget.org/packages/Withywoods.Selenium/)
[![Downloads](https://img.shields.io/nuget/dt/Withywoods.Selenium.svg)](https://www.nuget.org/packages/Withywoods.Selenium/)

New extension method to find an element with a wait: `driver.FindElement(By.ClassName("title"), 360);`.

[More information](./docs/Selenium.md).

#### Web application

[![Version](https://img.shields.io/nuget/v/Withywoods.AspNetCore.svg)](https://www.nuget.org/packages/Withywoods.AspNetCore/)
[![Downloads](https://img.shields.io/nuget/dt/Withywoods.AspNetCore.svg)](https://www.nuget.org/packages/Withywoods.AspNetCore/)

Easily add Swagger self-generated web page, only two lines in your Startup class!

```csharp
services.AddSwaggerGen(_webAppConfiguration); // in ConfigureServices()

app.UseSwagger(_webAppConfiguration); // in Configure()
```

[More information](./docs/WebApp.md).

#### Web testing

[![Version](https://img.shields.io/nuget/v/Withywoods.WebTesting.svg)](https://www.nuget.org/packages/Withywoods.WebTesting/)
[![Downloads](https://img.shields.io/nuget/dt/Withywoods.WebTesting.svg)](https://www.nuget.org/packages/Withywoods.WebTesting/)

Use Selenium web driver inside ASP.NET Integration tests? Yes, that's possible with `LocalServerFactory` class!

```csharp
public class SwaggerResourceTest : IClassFixture<LocalServerFactory<Startup>>, IDisposable
{
    [Fact]
    public void AspNetCoreApiSampleSwaggerResourceGet_ReturnsHttpOk()
    {
        // Arrange & Act
        _webDriver.Navigate().GoToUrl($"{_server.RootUri}/{_ResourceEndpoint}");

        // Assert
        _webDriver.FindElement(By.ClassName("title"), 360);
        _webDriver.Title.Should().Be("Swagger UI");
        _webDriver.FindElementByClassName("title").Text.Should().Contain("My API");
    }
}
```

Want to write easy API Rest tests? Sure, just use the `TestRunner` class!

```csharp
[Fact]
public async Task AspNetCoreApiSampleTaskResourceFullCycle_IsOk()
{
    var initialTasks = await _restRunner.GetResources<TaskDto>(_client);
    initialTasks.Count.Should().Be(0);

    var created = await _restRunner.CreateResource<TaskDto>(_client);

    await _restRunner.GetResourceById(created.Id, _client, created);

    await _restRunner.UpdateResource(created.Id, created, _client);

    var existingTasks = await _restRunner.GetResources<TaskDto>(_client);
    existingTasks.Count.Should().Be(1);

    await _restRunner.DeleteResource(created.Id, _client);

    var expectedNotFound = new ProblemDetails
    {
        Title = "Not Found",
        Status = 404,
        Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4"
    };
    await _restRunner.GetResourceById(created.Id, _client, expectedNotFound, HttpStatusCode.NotFound, config => config.Excluding(x => x.Extensions));

    var finalTasks = await _restRunner.GetResources<TaskDto>(_client);
    finalTasks.Count.Should().Be(0);
}
```

[More information](./docs/WebTesting.md).

## Build & Debug

- .NET Core SDK must be installed ([download](https://dotnet.microsoft.com/download))
  - Check the version from the command line `dotnet --version` (>= 2.2.104)
- Clone the repository (replace by the url if it was forked): `git clone https://github.com/devpro/withywoods.git`
- Enter the directory: `cd withywoods`
- Restore packages (NuGet): `dotnet restore`
- Build the solution: `dotnet run`
- Run the tests: `dotnet test`

## Samples

### AspNetCoreApiSample

This is a fully working example, with Swagger generation, API controllers, completely tested by integration tests.
