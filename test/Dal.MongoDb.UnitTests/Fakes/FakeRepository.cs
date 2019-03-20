using AutoMapper;
using Devpro.Withywoods.Dal.MongoDb.Repositories;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace Devpro.Withywoods.Dal.MongoDb.UnitTests.Fakes
{
    internal class FakeRepository : RepositoryBase
    {
        public FakeRepository(IMongoDbContext mongoDbContext, ILogger<FakeRepository> logger, IMapper mapper)
            : base(mongoDbContext, logger, mapper)
        {
        }

        protected override string CollectionName => "fake";

        public IMongoCollection<FakeEntity> GetCollection()
        {
            return GetCollection<FakeEntity>();
        }

        public ILogger<RepositoryBase> GetLogger()
        {
            return Logger;
        }

        public IMapper GetMapper()
        {
            return Mapper;
        }
    }
}
