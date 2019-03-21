# Devpro Withywoods - Shared .NET libraries

[![Build Status](https://dev.azure.com/devprofr/open-source/_apis/build/status/withywoods-CI?branchName=master)](https://dev.azure.com/devprofr/open-source/_build/latest?definitionId=12&branchName=master)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=devpro.withywoods&metric=alert_status)](https://sonarcloud.io/dashboard?id=devpro.withywoods)

## Components

### DAL MongoDB

[![Version](https://img.shields.io/nuget/v/Devpro.Withywoods.Dal.MongoDb.svg)](https://www.nuget.org/packages/Devpro.Withywoods.Dal.MongoDb/)
[![Downloads](https://img.shields.io/nuget/dt/Devpro.Withywoods.Dal.MongoDb.svg)](https://www.nuget.org/packages/Devpro.Withywoods.Dal.MongoDb/)

Sources for NuGet package [Devpro.Withywoods.Dal.MongoDb](https://www.nuget.org/packages/Devpro.Withywoods.Dal.MongoDb/) containing classes to ease the use of MongoDB (db server).

- Main classes:
  - `DefaultMongoDbContext`: have one clean database context in your application using the best practices
  - `RepositoryBase`: abstract class for repositories with common fields and methods
  - `ObjectIdToStringConverter` and `StringToObjectIdConverter`: AutoMapper converters

- Install the NuGet package: `Install-Package Devpro.Withywoods.Dal.MongoDb`.

- How to use it:
  - Implement `IMongoDbConfiguration` interface and add it in the service collection (dependency injection)
  - Use extension to register all needed types:

  ```csharp
  public void ConfigureServices(IServiceCollection services)
  {
    // ...

	services.AddScoped<Devpro.Withywoods.Dal.MongoDb.IMongoDbConfiguration, MyCustomConfiguration>();
    service.AddMongoDbContext();

	// ...
  }
  ```

  - Register AutoMapper converters:

  ```csharp
	var config = new MapperConfiguration(x =>
	{
		// ...
		x.CreateMap<MongoDB.Bson.ObjectId, string>()
			.ConvertUsing<Devpro.Withywoods.Dal.MongoDb.MappingConverters.ObjectIdToStringConverter>();
		x.CreateMap<string, MongoDB.Bson.ObjectId>()
			.ConvertUsing<Devpro.Withywoods.Dal.MongoDb.MappingConverters.StringToObjectIdConverter>();
		x.AllowNullCollections = true;
	});

    var mapper = config.CreateMapper();
    mapper.ConfigurationProvider.AssertConfigurationIsValid();
  ```
