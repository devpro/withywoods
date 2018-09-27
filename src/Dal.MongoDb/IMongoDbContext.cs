using MongoDB.Driver;

namespace Devpro.Withywoods.Dal.MongoDb
{
    /// <summary>
    /// MongoDB Context interface.
    /// </summary>
    public interface IMongoDbContext
    {
        /// <summary>
        /// Get database.
        /// </summary>
        /// <param name="databaseName"></param>
        /// <returns></returns>
        IMongoDatabase GetDatabase(string databaseName = null);
    }
}
