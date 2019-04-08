# Withywoods MongoDB DAL library

This library can be used by an .NET application.

## Features

- Main classes:
  - `DefaultMongoDbContext`: have one clean database context in your application using the best practices
  - `RepositoryBase`: abstract class for repositories with common fields and methods
  - `ObjectIdToStringConverter` and `StringToObjectIdConverter`: AutoMapper converters

- How to use it:
  - Install `Withywoods.Dal.MongoDb` package from NuGet
  - Implement `IMongoDbConfiguration` interface (let's call it MyCustomConfiguration)
  - Use the extension to register all needed types (in Startup.cs file):

  ```csharp
    // Add this line in Startup class in ConfigureServices method (MyCustom)
    service.AddMongoDbContext<MyCustomConfiguration>();
  ```

  - Use RepositoryBase asbtract class on your repositories and enjoy querying MongoDB!

  ```csharp
    public class UserRepository : RepositoryBase
    {
        public UserRepository(IMongoDbContext mongoDbContext, ILogger<UserRepository> logger, IMapper mapper)
            : base(mongoDbContext, logger, mapper)
        {
        }

        public async Task<List<UserModel>> FindAllAsync()
        {
            var collection = GetCollection<User>();
            var results = collection.Find({});
            return Mapper.Map<List<UserModel>>(await results.ToListAsync());
        }
    }
  ```

  - (Optional) register AutoMapper converters:

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
