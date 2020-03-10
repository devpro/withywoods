# RabbitMQ adapter library

[![Version](https://img.shields.io/nuget/v/Withywoods.RabbitMq.svg)](https://www.nuget.org/packages/Withywoods.RabbitMq/)
[![Downloads](https://img.shields.io/nuget/dt/Withywoods.RabbitMq.svg)](https://www.nuget.org/packages/Withywoods.RabbitMq/)

The goal of this library is to ease the use of RabbitMQ in a .NET program and gather best practices.

## References

- [rabbitmq.com](https://www.rabbitmq.com/)
  - [.NET/C# RabbitMQ Client Library](https://www.rabbitmq.com/dotnet.html)
- [GitHub](https://github.com/rabbitmq)
  - [rabbitmq/rabbitmq-dotnet-client](https://github.com/rabbitmq/rabbitmq-dotnet-client)
  - [rabbitmq/rabbitmq-tutorials](https://github.com/rabbitmq/rabbitmq-tutorials/tree/master/dotnet)
- [NuGet](https://www.nuget.org/packages/RabbitMQ.Client)

## How to use

- Have the [NuGet package](https://www.nuget.org/packages/Withywoods.RabbitMq) in your csproj file (can be done manually, with Visual Studio or through nuget command)

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <ItemGroup>
    <PackageReference Include="Withywoods.RabbitMq" Version="X.Y.Z" />
  </ItemGroup>
</Project>
```

- Make the code changes to be able to use the library (config & service provider)

```csharp
// implement the configuration interface (for instance in a configuration class in your app project), or use DefaultRabbitMqConfiguration
using Withywoods.RabbitMq;

public class AppConfiguration : IRabbitMqConfiguration
{
    // explicitely choose where to take the configuration (this is the responibility of the app, not the library)
}

// configure your service provider (for instance in your app Startup class)
using Withywoods.RabbitMq.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection()
    .AddLogging()
    .AddRabbitMqFactory(Configuration);
```

- Use the channel factory

```csharp
using Withywoods.RabbitMq;

private readonly IChannelFactory _channelFactory;

public MyService(IChannelFactory channelFactory)
{
    _channelFactory = channelFactory;
}

public async Task DoStuff()
{
    using var channel = channelFactory.Create();
}
```

## How to debug

- Have an instance of RabbitMQ

```bash
docker run -d --rm -p 5672:5672 --hostname rabbit.local --name rabbitmq382 rabbitmq:3.8.2
```

## How to run the samples

```bash
# from command window #1
dotnet run --project samples\RabbitMqConsumerSample host.docker.internal 5672

# from command window #2
dotnet run --project samples\RabbitMqPublisherSample host.docker.internal 5672
```
