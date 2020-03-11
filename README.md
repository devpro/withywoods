# Devpro Withywoods - Shared .NET libraries

[![Build Status](https://dev.azure.com/devprofr/open-source/_apis/build/status/libraries/withywoods-ci?branchName=master)](https://dev.azure.com/devprofr/open-source/_build/latest?definitionId=31&branchName=master)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=withywoods&metric=alert_status)](https://sonarcloud.io/dashboard?id=withywoods)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=withywoods&metric=coverage)](https://sonarcloud.io/dashboard?id=withywoods)

Whithywoods is a set of small independant .NET libraries (Standard/Core). The goal is to do better with less code and capitalize on best practices (#KISS #DRY).

All libraries are available on [nuget.org](https://www.nuget.org/). Feel free to report any issue or ask for a change. You can also contribute with Pull Requests on GitHub!

NB: The name _Whithywoods_ comes from [Robin Hobb](https://twitter.com/robinhobb)'s incredible writing.

## How to use

### Common / Configuration library

[![Version](https://img.shields.io/nuget/v/Withywoods.Configuration.svg)](https://www.nuget.org/packages/Withywoods.Configuration/)
[![Downloads](https://img.shields.io/nuget/dt/Withywoods.Configuration.svg)](https://www.nuget.org/packages/Withywoods.Configuration/)

_tl;dr_ New extension method to access configuration: `configuration.TryGetSection()`

[More information](./src/Configuration/README.md)

### Common / Net HTTP library

[![Version](https://img.shields.io/nuget/v/Withywoods.Net.Http.svg)](https://www.nuget.org/packages/Withywoods.Net.Http/)
[![Downloads](https://img.shields.io/nuget/dt/Withywoods.Net.Http.svg)](https://www.nuget.org/packages/Withywoods.Net.Http/)

_tl;dr_ New exception: `ConnectivityException`

[More information](./src/Net.Http/README.md)

### Common / Serialization library

[![Version](https://img.shields.io/nuget/v/Withywoods.Serialization.svg)](https://www.nuget.org/packages/Withywoods.Serialization/)
[![Downloads](https://img.shields.io/nuget/dt/Withywoods.Serialization.svg)](https://www.nuget.org/packages/Withywoods.Serialization/)

_tl;dr_ New extension methods to serialize/deserialize from Json: `myObject.ToJson()` and `myString.FromJson()`

[More information](./src/Serialization/README.md)

### Common / System library

[![Version](https://img.shields.io/nuget/v/Withywoods.System.svg)](https://www.nuget.org/packages/Withywoods.System/)
[![Downloads](https://img.shields.io/nuget/dt/Withywoods.System.svg)](https://www.nuget.org/packages/Withywoods.System/)

_tl;dr_ New string extensions: `myString.FirstCharToUpper()`

[More information](./src/System/README.md)

### Data Access / MongoDB library

[![Version](https://img.shields.io/nuget/v/Withywoods.Dal.MongoDb.svg)](https://www.nuget.org/packages/Withywoods.Dal.MongoDb/)
[![Downloads](https://img.shields.io/nuget/dt/Withywoods.Dal.MongoDb.svg)](https://www.nuget.org/packages/Withywoods.Dal.MongoDb/)

_tl;dr_ Get access to a MongoDB database in a few lines by using best practices.

[More information](./src/Dal.MongoDb/README.md)

### Message Broker / RabbitMQ library

[![Version](https://img.shields.io/nuget/v/Withywoods.RabbitMq.svg)](https://www.nuget.org/packages/Withywoods.RabbitMq/)
[![Downloads](https://img.shields.io/nuget/dt/Withywoods.RabbitMq.svg)](https://www.nuget.org/packages/Withywoods.RabbitMq/)

_tl;dr_ Clean channel factory to ease the use of RabbitMQ as well as enabling decoupling through interfaces.

[More information](./src/RabbitMq/README.md)

### Web / Selenium library

[![Version](https://img.shields.io/nuget/v/Withywoods.Selenium.svg)](https://www.nuget.org/packages/Withywoods.Selenium/)
[![Downloads](https://img.shields.io/nuget/dt/Withywoods.Selenium.svg)](https://www.nuget.org/packages/Withywoods.Selenium/)

_tl;dr_ New extension method to find an element with a wait: `driver.FindElement(By.ClassName("title"), 360);`.

[More information](./src/Selenium/README.md)

### Web / Web Application library

[![Version](https://img.shields.io/nuget/v/Withywoods.AspNetCore.svg)](https://www.nuget.org/packages/Withywoods.AspNetCore/)
[![Downloads](https://img.shields.io/nuget/dt/Withywoods.AspNetCore.svg)](https://www.nuget.org/packages/Withywoods.AspNetCore/)

_tl;dr_ Easily add Swagger self-generated web page, only two lines in your Startup class!

```csharp
services.AddSwaggerGen(_webAppConfiguration); // in ConfigureServices()

app.UseSwagger(_webAppConfiguration); // in Configure()
```

[More information](./src/AspNetCore/README.md)

### Web / Web Testing library

[![Version](https://img.shields.io/nuget/v/Withywoods.WebTesting.svg)](https://www.nuget.org/packages/Withywoods.WebTesting/)
[![Downloads](https://img.shields.io/nuget/dt/Withywoods.WebTesting.svg)](https://www.nuget.org/packages/Withywoods.WebTesting/)

_tl;dr_ Use Selenium web driver inside ASP.NET Integration tests? Yes, that's possible with `LocalServerFactory` class!

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

[More information](./src/WebTesting/README.md)

## How to build

```bash
# check .NET Core SDK is installed (download from https://dotnet.microsoft.com/download)
dotnet --version

# restore NuGet packages
dotnet restore

# build the solution
dotnet build
```

## How to test

/!\ MongoDB DAL integration tests require a local MongoDB server (through Docker for instance)

```bash
dotnet test
```

## Samples

### AspNetCoreApiSample

This is a fully working example, with Swagger generation, API controllers, completely tested by integration tests.

### RabbitMQ

There are two console projects to publish and consumes messages through RabbitMQ, using Withywoods RabbitMQ library.
