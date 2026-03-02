# Withywoods AutoMapper MongoDB library

This library can be used by an .NET application.

## How to use it

- Install `Withywoods.AutoMapper.MongoDb` package from NuGet
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
