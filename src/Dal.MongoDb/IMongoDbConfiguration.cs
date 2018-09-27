namespace Devpro.Withywoods.Dal.MongoDb
{
    /// <summary>
    /// Configuration interface in order to use DAL MongoDB.
    /// </summary>
    public interface IMongoDbConfiguration
    {
        /// <summary>
        /// MongoDB connection string.
        /// </summary>
        string ConnectionString { get; }

        /// <summary>
        /// Database name.
        /// </summary>
        string DatabaseName { get; }
    }
}
