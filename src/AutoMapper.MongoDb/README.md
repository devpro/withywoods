# Withywoods AutoMapper MongoDB library

[![Version](https://img.shields.io/nuget/v/Withywoods.AutoMapper.MongoDb.svg)](https://www.nuget.org/packages/Withywoods.AutoMapper.MongoDb/)
[![Downloads](https://img.shields.io/nuget/dt/Withywoods.AutoMapper.MongoDb.svg)](https://www.nuget.org/packages/Withywoods.AutoMapper.MongoDb/)

The `Withywoods.AutoMapper.MongoDb` package provides:

- Two mapper converters:
  - `ObjectIdToStringConverter`
  - `StringToObjectIdConverter`

## How to use it

- Install `Withywoods.AutoMapper.MongoDb` package from NuGet
- Register AutoMapper converters:

  ```csharp
  var config = new MapperConfiguration(x =>
  {
      x.CreateMap<MongoDB.Bson.ObjectId, string>()
        .ConvertUsing<Withywoods.AutoMapper.MongoDb.ObjectIdToStringConverter>();
      x.CreateMap<string, MongoDB.Bson.ObjectId>()
        .ConvertUsing<Withywoods.AutoMapper.MongoDb.StringToObjectIdConverter>();
      x.AllowNullCollections = true;
  });

  var mapper = config.CreateMapper();
  mapper.ConfigurationProvider.AssertConfigurationIsValid();
  ```
