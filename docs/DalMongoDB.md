# Withywoods MongoDB DAL library

This library can be used by an .NET application supporting .NET Standard 2.0.

## Features

- Main classes:
  - `DefaultMongoDbContext`: have one clean database context in your application using the best practices
  - `RepositoryBase`: abstract class for repositories with common fields and methods
  - `ObjectIdToStringConverter` and `StringToObjectIdConverter`: AutoMapper converters

- How to use it:
  - Reference `Withywoods.Dal.MongoDb` NuGet package
  - Implement `IMongoDbConfiguration` interface (let's call it MyCustomConfiguration
  - Use the extension to register all needed types:

  ```csharp
    // Add this line in Startup class in ConfigureServices method (MyCustom)
    service.AddMongoDbContext<MyCustomConfiguration>();
  ```

  - Register AutoMapper converters:

  ```csharp
    var config = new MapperConfiguration(x =>
    {
        x.CreateMap<MongoDB.Bson.ObjectId, string>()
	        .ConvertUsing<Withywoods.Dal.MongoDb.MappingConverters.ObjectIdToStringConverter>();
		x.CreateMap<string, MongoDB.Bson.ObjectId>()
			.ConvertUsing<Withywoods.Dal.MongoDb.MappingConverters.StringToObjectIdConverter>();
		x.AllowNullCollections = true;
	});

    var mapper = config.CreateMapper();
    mapper.ConfigurationProvider.AssertConfigurationIsValid();
  ```
