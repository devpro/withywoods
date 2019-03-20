using MongoDB.Driver;

namespace Devpro.Withywoods.Dal.MongoDb
{
    /// <summary>
    /// Factory for <see cref="IMongoClient"/>.
    /// </summary>
    public class MongoClientFactory : IMongoClientFactory
    {
        #region IMongoClientFactory Methods

        public IMongoClient CreateClient(string connectionString)
        {
            return new MongoClient(connectionString);
        }

        #endregion
    }
}
