using MongoDB.Driver;

namespace Devpro.Withywoods.Dal.MongoDb
{
    /// <summary>
    /// Interface for factory on <see cref="IMongoClient"/>.
    /// </summary>
    public interface IMongoClientFactory
    {
        /// <summary>
        /// Create a new MongoDB client by using the given configuration.
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        IMongoClient CreateClient(string connectionString);
    }
}
