using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace Devpro.Withywoods.Dal.MongoDb
{
    /// <summary>
    /// Default MongoDB context implementation.
    /// </summary>
    public class DefaultMongoDbContext : IMongoDbContext
    {
        private readonly string _databaseName;

        /// <summary>
        /// Mongo client
        /// </summary>
        protected MongoClient MongoClient { get; private set; }

        /// <summary>
        /// Create a new instance of <see cref="DefaultMongoDbContext"/>.
        /// </summary>
        /// <param name="configuration"></param>
        public DefaultMongoDbContext(IMongoDbConfiguration configuration)
        {
            _databaseName = configuration.DatabaseName;
            RegisterConventions();
            MongoClient = new MongoClient(configuration.ConnectionString);
        }

        /// <summary>
        /// Get Database instance.
        /// </summary>
        /// <param name="databaseName"></param>
        /// <returns></returns>
        public IMongoDatabase GetDatabase(string databaseName = null)
        {
            return MongoClient.GetDatabase(databaseName ?? _databaseName);
        }

        /// <summary>
        /// Register default conventions in the MongoDB registry.
        /// </summary>
        private void RegisterConventions()
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
