using System.Linq;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using Withywoods.Dal.MongoDb.Serialization;

namespace Withywoods.Dal.MongoDb
{
    /// <summary>
    /// Default MongoDB context implementation.
    /// </summary>
    public class DefaultMongoDbContext : IMongoDbContext
    {
        private readonly IMongoDbConfiguration _mongoDbConfiguration;

        /// <summary>
        /// Mongo client
        /// </summary>
        protected IMongoClient MongoClient { get; private set; }

        /// <summary>
        /// Create a new instance of <see cref="DefaultMongoDbContext"/>.
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="mongoClientFactory"></param>
        public DefaultMongoDbContext(IMongoDbConfiguration configuration, IMongoClientFactory mongoClientFactory)
        {
            _mongoDbConfiguration = configuration;
            RegisterConventions();
            MongoClient = mongoClientFactory.CreateClient(configuration.ConnectionString);
        }

        /// <summary>
        /// Get Database instance.
        /// </summary>
        /// <param name="databaseName"></param>
        /// <returns></returns>
        public IMongoDatabase GetDatabase(string databaseName = null)
        {
            return MongoClient.GetDatabase(databaseName ?? _mongoDbConfiguration.DatabaseName);
        }

        /// <summary>
        /// Register the serialization conventions in the registry from the list provided by the configuration.
        /// </summary>
        private void RegisterConventions()
        {
            if (_mongoDbConfiguration.SerializationConventions == null
                || !_mongoDbConfiguration.SerializationConventions.Any())
            {
                return;
            }

            if (_mongoDbConfiguration.SerializationConventions.Contains(ConventionValues.IgnoreNullValues))
            {
                ConventionRegistry.Register(
                    ConventionValues.IgnoreNullValues,
                    new ConventionPack
                    {
                        new IgnoreIfNullConvention(true)
                    },
                    t => true
                );
            }

            if (_mongoDbConfiguration.SerializationConventions.Contains(ConventionValues.CamelCaseElementName))
            {
                ConventionRegistry.Register(
                    ConventionValues.CamelCaseElementName,
                    new ConventionPack
                    {
                        new CamelCaseElementNameConvention()
                    },
                    t => true
                );
            }

            if (_mongoDbConfiguration.SerializationConventions.Contains(ConventionValues.EnumAsString))
            {
                ConventionRegistry.Register(
                    ConventionValues.EnumAsString,
                    new ConventionPack
                    {
                        new EnumRepresentationConvention(BsonType.String)
                    },
                    t => true
                );
            }

            if (_mongoDbConfiguration.SerializationConventions.Contains(ConventionValues.IgnoreExtraElements))
            {
                ConventionRegistry.Register(
                    ConventionValues.IgnoreExtraElements,
                    new ConventionPack
                    {
                        new IgnoreExtraElementsConvention(true)
                    },
                    t => true
                );
            }
        }
    }
}
