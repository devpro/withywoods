using AutoMapper;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace Withywoods.Dal.MongoDb.Repositories
{
    /// <summary>
    /// Base class for DAL repositories using MongoDB as DB server.
    /// </summary>
    public abstract class RepositoryBase
    {
        /// <summary>
        /// MongoDB context.
        /// </summary>
        protected IMongoDbContext MongoDbContext { get; private set; }

        /// <summary>
        /// MongoDB database.
        /// </summary>
        protected IMongoDatabase MongoDatabase { get; private set; }

        /// <summary>
        /// Logger.
        /// </summary>
        protected ILogger<RepositoryBase> Logger { get; private set; }

        /// <summary>
        /// AutoMapper.
        /// </summary>
        protected IMapper Mapper { get; private set; }

        /// <summary>
        /// Collection name.
        /// </summary>
        protected abstract string CollectionName { get; }

        /// <summary>
        /// Create a new instance of <see cref="RepositoryBase"/>.
        /// </summary>
        /// <param name="mongoDbContext"></param>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        protected RepositoryBase(IMongoDbContext mongoDbContext, ILogger<RepositoryBase> logger, IMapper mapper)
        {
            MongoDbContext = mongoDbContext;
            MongoDatabase = MongoDbContext.GetDatabase();
            Logger = logger;
            Mapper = mapper;
        }

        /// <summary>
        /// Get collection of <see cref="T"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected IMongoCollection<T> GetCollection<T>()
        {
            return MongoDatabase.GetCollection<T>(CollectionName);
        }
    }
}
