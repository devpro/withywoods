using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace Devpro.Withywoods.Dal.MongoDb
{
    public class DefaultMongoDbContext : IMongoDbContext
    {
        private readonly string _databaseName;

        protected MongoClient MongoClient { get; set; }

        public DefaultMongoDbContext(IMongoDbConfiguration configuration)
        {
            _databaseName = configuration.DatabaseName;
            RegisterConventions();
            MongoClient = new MongoClient(configuration.ConnectionString);
        }

        public IMongoDatabase GetDatabase(string databaseName = null)
        {
            return MongoClient.GetDatabase(databaseName ?? _databaseName);
        }

        protected virtual void RegisterConventions()
        {
            ConventionRegistry.Register(
                "IgnoreNullValues",
                new ConventionPack
                {
                    new IgnoreIfNullConvention(true)
                },
                t => true);
            ConventionRegistry.Register(
                "CamelCaseElementName",
                new ConventionPack
                {
                    new CamelCaseElementNameConvention()
                },
                t => true);
            ConventionRegistry.Register(
                "EnumAsString",
                new ConventionPack
                {
                    new EnumRepresentationConvention(BsonType.String)
                },
                t => true);
            ConventionRegistry.Register(
                "IgnoreExtraElements",
                new ConventionPack
                {
                    new IgnoreExtraElementsConvention(true)
                },
                t => true);
        }
    }
}
