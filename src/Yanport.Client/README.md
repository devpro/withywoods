# Yanport .NET Client

[![Build Status](https://dev.azure.com/devprofr/open-source/_apis/build/status/libraries/yanport-dotnet-client-ci?branchName=master)](https://dev.azure.com/devprofr/open-source/_build/latest?definitionId=35&branchName=master)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=devpro.yanport.dotnetclient&metric=alert_status)](https://sonarcloud.io/dashboard?id=devpro.yanport.dotnetclient)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=devpro.yanport.dotnetclient&metric=coverage)](https://sonarcloud.io/dashboard?id=devpro.yanport.dotnetclient)

.NET client for [Yanport](https://www.yanport.com/), provider of real estate data.

Package | Version | Type
------- | ------- | ----
`Devpro.Yanport.Abstractions` | [![Version](https://img.shields.io/nuget/v/Devpro.Yanport.Abstractions.svg)](https://www.nuget.org/packages/Devpro.Yanport.Abstractions/) | .NET Standard 2.1
`Devpro.Yanport.Client` | [![Version](https://img.shields.io/nuget/v/Devpro.Yanport.Client.svg)](https://www.nuget.org/packages/Devpro.Yanport.Client/) | .NET Standard 2.1

## How to use

- Have the [NuGet package](https://www.nuget.org/packages/Devpro.Yanport.Client) in your csproj file (can be done manually, with Visual Studio or through nuget command)

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <ItemGroup>
    <PackageReference Include="Devpro.Yanport.Client" Version="X.Y.Z" />
  </ItemGroup>
</Project>
```

- Make the code changes to be able to use the library (config & service provider)

```csharp
// implement the configuration interface (for instance in a configuration class in your app project) or use DefaultYanportClientConfiguration
using Devpro.Yanport.Client;

public class AppConfiguration : IYanportClientConfiguration
{
    // explicitely choose where to take the configuration for Yanport REST API (this is the responibility of the app, not the library)
}

// configure your service provider (for instance in your app Startup class)
using Devpro.Yanport.Client.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection()
  .AddLogging()
  .AddYanportClient(Configuration);
```

- Use the repositories (enjoy a simple, yet optimized, HTTP client)

```csharp
using Devpro.Yanport.Client;

private readonly IPropertRepository _propertyRepository;

public MyService(IPropertRepository propertyRepository)
{
    _propertyRepository = propertyRepository;
}

public async Task GetProperties()
{
    var properties = await _propertyRepository.FindAllAsync();
}
```

## How to build

Once the git repository has been cloned, execute the following commands from the root directory:

```bash
dotnet restore
dotnet build
```

## How to test

For integration tests, to manage the configuration (secrets) you can create a file at the root directory called `Local.runsettings` or define them as environment variables:

```xml
<?xml version="1.0" encoding="utf-8"?>
<RunSettings>
  <RunConfiguration>
    <EnvironmentVariables>
      <Yanport__Sandbox__BaseUrl>https://api.yanport.com</Yanport__Sandbox__BaseUrl>
      <Yanport__Sandbox__Token>xxx</Yanport__Sandbox__Token>
    </EnvironmentVariables>
  </RunConfiguration>
</RunSettings>
```

And execute all tests (unit and integration ones):

```bash
dotnet test --settings Local.runsettings
```

## References

- [API Documentation](https://www.yanport.com/data/api/documentation)
