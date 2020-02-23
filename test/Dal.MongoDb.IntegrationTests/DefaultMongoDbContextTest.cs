using System.Threading.Tasks;
using FluentAssertions;
using MongoDB.Bson;
using MongoDB.Driver;
using Xunit;

namespace Withywoods.Dal.MongoDb.IntegrationTests
{
    [Trait("Environment", "Localhost")]
    public class DefaultMongoDbContextTest : IAsyncLifetime
    {
        private readonly IMongoDbConfiguration _configuration;
        private readonly IMongoClientFactory _mongoClientFactory;
        private readonly IMongoDbContext _mongoDbContext;

        public DefaultMongoDbContextTest()
        {
            _configuration = new MongoDbConfiguration();
            _mongoClientFactory = new MongoClientFactory();
            _mongoDbContext = new DefaultMongoDbContext(_configuration, _mongoClientFactory);
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
        public async Task CrudOperationWithServiceProvider_OnInsertOneAndFind_ShouldReturnInsertedDocument()
        {
            // Arrange
            var database = _mongoDbContext.GetDatabase(_configuration.DatabaseName);
            var collection = database.GetCollection<BsonDocument>("bar2");

            // Act
            await collection.InsertOneAsync(new BsonDocument("name", "Jacky"));
            var entities = await collection.Find(new BsonDocument("name", "Jacky"))
                .ToListAsync();

            // Assert
            entities.Should().NotBeNullOrEmpty();
            entities[0]["_id"].AsObjectId.Should().NotBeNull();
            entities[0]["name"].Should().Be("Jacky");
        }

        private async Task CleanDatabaseCollection()
        {
            var client = _mongoClientFactory.CreateClient(_configuration.ConnectionString);
            await client.DropDatabaseAsync(_configuration.DatabaseName);
        }
    }
}
