# HubSpot .NET

.NET client for [HubSpot](https://www.hubspot.com/) API.

## Requirements

- Have an HubSpot developer account ([create one](https://developers.hubspot.com/))

## How to use

- Have the [NuGet package](https://www.nuget.org/packages/Withywoods.Hubspot.Client) in your csproj file (can be done manually, with the IDE or through nuget command)

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <ItemGroup>
    <PackageReference Include="Withywoods.Hubspot.Client" Version="X.Y.Z" />
  </ItemGroup>
</Project>
```

- Make the code changes to be able to use the library (config & service provider)

```csharp
// implement the configuration interface (for instance in a configuration class in your app project) or use DefaultHubspotClientConfiguration
using Withywoods.Hubspot.Client;

public class AppConfiguration : IHubspotClientConfiguration
{
    // explicitely choose where to take the configuration for Hubspot REST API (this is the responibility of the app, not the library)
}

// configure your service provider (for instance in your app Startup class)
using Devpro.Hubspot.Client.DependencyInjection;

var services = new ServiceCollection()
  .AddHubspotClient(Configuration);
```

- Use the repositories (enjoy a simple, yet optimized, HTTP client)

```csharp
using Withywoods.Hubspot.Abstractions;

private readonly IContactRepository _contactRepository;

public MyService(IContactRepository contactRepository)
{
    _contactRepository = contactRepository;
}

public async Task GetContacts()
{
    var contacts = await _contactRepository.FindAllAsync();
}
```

## References

- [HubSpot API Overview](https://developers.hubspot.com/docs/overview)
- [adimichele/hubspot-ruby](https://github.com/adimichele/hubspot-ruby)
- [HubSpot/hubspot-php](https://github.com/HubSpot/hubspot-php)
- [hubspot-net/HubSpot.NET](https://github.com/hubspot-net/HubSpot.NET): outdated .NET client, no recent CI with code coverage, not compatible with dependency injection and not using .NET best practices (HTTP client, naming convention, clean code, editorconfig)
