# Devpro Withywoods - Shared .NET libraries

[![Build Status](https://dev.azure.com/devprofr/open-source/_apis/build/status/withywoods-CI?branchName=master)](https://dev.azure.com/devprofr/open-source/_build/latest?definitionId=12&branchName=master)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=devpro.withywoods&metric=alert_status)](https://sonarcloud.io/dashboard?id=devpro.withywoods)

.NET libraries (Standard/Core) to ease .NET development needs. All this libraries are available on [nuget.org](https://www.nuget.org/).

## Get started

### Data Access Layer (DAL) for MongoDB

[![Version](https://img.shields.io/nuget/v/Devpro.Withywoods.Dal.MongoDb.svg)](https://www.nuget.org/packages/Devpro.Withywoods.Dal.MongoDb/)
[![Downloads](https://img.shields.io/nuget/dt/Devpro.Withywoods.Dal.MongoDb.svg)](https://www.nuget.org/packages/Devpro.Withywoods.Dal.MongoDb/)

- Main classes:
  - `DefaultMongoDbContext`: have one clean database context in your application using the best practices
  - `RepositoryBase`: abstract class for repositories with common fields and methods
  - `ObjectIdToStringConverter` and `StringToObjectIdConverter`: AutoMapper converters

- How to use it:
  - Reference `Devpro.Withywoods.Dal.MongoDb` NuGet package
  - Implement `IMongoDbConfiguration` interface and add it in the service collection (dependency injection)
  - Use extension to register all needed types:

  ```csharp
    // Add this line in Startup class in ConfigureServices method
    service.AddMongoDbContext<Devpro.Withywoods.Dal.MongoDb.IMongoDbConfiguration>();
  ```

  - Register AutoMapper converters:

  ```csharp
    var config = new MapperConfiguration(x =>
    {
        x.CreateMap<MongoDB.Bson.ObjectId, string>()
	        .ConvertUsing<Devpro.Withywoods.Dal.MongoDb.MappingConverters.ObjectIdToStringConverter>();
		x.CreateMap<string, MongoDB.Bson.ObjectId>()
			.ConvertUsing<Devpro.Withywoods.Dal.MongoDb.MappingConverters.StringToObjectIdConverter>();
		x.AllowNullCollections = true;
	});

    var mapper = config.CreateMapper();
    mapper.ConfigurationProvider.AssertConfigurationIsValid();
  ```

## Build & Debug

- .NET Core SDK must be installed ([download](https://dotnet.microsoft.com/download))
  - From the command line `dotnet --version` must return a version >= 2.2.104
- Build the solution: `dotnet run`
- Run the tests: `dotnet test`
