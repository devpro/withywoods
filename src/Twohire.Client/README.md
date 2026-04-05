# 2hire .NET Client

.NET client for [2hire.io](https://2hire.io/) solution to manage a fleet of connected vehicles.

## How to use

- Have the [NuGet package](https://www.nuget.org/packages/Withywoods.Twohire.Client) in your csproj file (can be done manually, with Visual Studio or through nuget command)

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <ItemGroup>
    <PackageReference Include="Withywoods.Twohire.Client" Version="X.Y.Z" />
  </ItemGroup>
</Project>
```

- Make the code changes to be able to use the library (config & service provider)

```csharp
// implement the configuration interface (for instance in a configuration class in your app project) or use DefaultTwohireClientConfiguration
using Withywoods.Twohire.Client;

public class AppConfiguration : ITwohireClientConfiguration
{
    // explicitely choose where to take the configuration for 2hire REST API (this is the responibility of the app, not the library)
}

// configure your service provider (for instance in your app Startup class)
using Withywoods.Twohire.Client.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection()
  .AddLogging()
  .AddTwohireClient(Configuration);
```

- Use the repositories (enjoy a simple, yet optimized, HTTP client)

```csharp
using Withywoods.Twohire.Abstractions.Repositories;

private readonly IPersonalVehicleRepository _personalVehicleRepository;

public MyService(IPersonalVehicleRepository personalVehicleRepository)
{
    _personalVehicleRepository = personalVehicleRepository;
}

public async Task GetVehicles()
{
    var vehicles = await _personalVehicleRepository.FindAllAsync();
}
```
