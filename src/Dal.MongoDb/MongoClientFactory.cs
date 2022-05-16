using MongoDB.Driver;

namespace Withywoods.Dal.MongoDb
{
    /// <summary>
    /// Factory for <see cref="IMongoClient"/>.
    /// </summary>
    public class MongoClientFactory : IMongoClientFactory
    {
        public IMongoClient CreateClient(string connectionString)
        {
            return new MongoClient(connectionString);
        }
    }
}
