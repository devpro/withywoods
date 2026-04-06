# Yanport .NET Client

.NET client for [Yanport](https://www.yanport.com/), provider of real estate data.

## How to use

- Have the [NuGet package](https://www.nuget.org/packages/Devpro.Yanport.Client) in your csproj file (can be done manually, with Visual Studio or through nuget command)

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <ItemGroup>
    <PackageReference Include="Withywoods.Yanport.Client" Version="X.Y.Z" />
  </ItemGroup>
</Project>
```

- Make the code changes to be able to use the library (config & service provider)

```csharp
// implement the configuration interface (for instance in a configuration class in your app project) or use DefaultYanportClientConfiguration
using Withywoods.Yanport.Client;

public class AppConfiguration : IYanportClientConfiguration
{
    // explicitely choose where to take the configuration for Yanport REST API (this is the responibility of the app, not the library)
}

// configure your service provider (for instance in your app Startup class)
using Withywoods.Yanport.Client.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection()
  .AddLogging()
  .AddYanportClient(Configuration);
```

- Use the repositories (enjoy a simple, yet optimized, HTTP client)

```csharp
using Withywoods.Yanport.Client;

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

## References

- [API Documentation](https://www.yanport.com/data/api/documentation)
