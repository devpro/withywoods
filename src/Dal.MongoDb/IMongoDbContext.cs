using MongoDB.Driver;

namespace Devpro.Withywoods.Dal.MongoDb
{
    /// <summary>
    /// MongoDB Context interface.
    /// </summary>
    public interface IMongoDbContext
    {
        /// <summary>
        /// Get database instance.
        /// </summary>
        /// <param name="databaseName">database name (can be null)</param>
        /// <returns></returns>
        IMongoDatabase GetDatabase(string databaseName = null);
    }
}
