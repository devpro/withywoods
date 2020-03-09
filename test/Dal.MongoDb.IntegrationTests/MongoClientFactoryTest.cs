using System.Threading.Tasks;
using FluentAssertions;
using MongoDB.Bson;
using MongoDB.Driver;
using Xunit;

namespace Withywoods.Dal.MongoDb.IntegrationTests
{
    [Trait("Environment", "Localhost")]
    public class MongoClientFactoryTest : IAsyncLifetime
    {
        private readonly IMongoDbConfiguration _configuration;
        private readonly MongoClientFactory _mongoClientFactory;

        public MongoClientFactoryTest()
        {
            _configuration = new MongoDbConfiguration();
            _mongoClientFactory = new MongoClientFactory();
        }

        public Task InitializeAsync()
        {
            return CleanDatabaseCollection();
        }

        public Task DisposeAsync()
        {
            return CleanDatabaseCollection();
        }

        [Fact]
        public async Task MongoClientFactory_OnInsertOneAndFind_ShouldReturnInsertedDocument()
        {
            // Arrange
            var client = _mongoClientFactory.CreateClient(_configuration.ConnectionString);
            var database = client.GetDatabase(_configuration.DatabaseName);
            var collection = database.GetCollection<BsonDocument>("bar");

            // Act
            await collection.InsertOneAsync(new BsonDocument("name", "Jack"));
            var entities = await collection.Find(new BsonDocument("name", "Jack"))
                .ToListAsync();

            // Assert
            entities.Should().NotBeNullOrEmpty();
            entities[0]["_id"].AsObjectId.Should().NotBeNull();
            entities[0]["name"].Should().Be("Jack");
        }

        private async Task CleanDatabaseCollection()
        {
            var client = _mongoClientFactory.CreateClient(_configuration.ConnectionString);
            await client.DropDatabaseAsync(_configuration.DatabaseName);
        }
    }
}
