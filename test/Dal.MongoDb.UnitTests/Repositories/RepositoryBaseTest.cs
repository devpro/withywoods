using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Moq;
using Withywoods.Dal.MongoDb.UnitTests.Fakes;
using Xunit;

namespace Withywoods.Dal.MongoDb.UnitTests.Repositories
{
    [Trait("Category", "UnitTests")]
    public class RepositoryBaseTest
    {
        #region Private fields & Constructor

        private readonly ServiceProvider _serviceProvider;

        public RepositoryBaseTest()
        {
            var services = new ServiceCollection()
                .AddLogging();
            _serviceProvider = services.BuildServiceProvider();
        }

        #endregion

        #region GetCollection test methods

        [Fact]
        public void RepositoryBaseGetCollection_ReturnValidCollection()
        {
            // Arrange
            var repository = BuildRepository();

            // Act
            var output = repository.GetCollection();

            // Assert
            output.Should().NotBeNull();
        }

        #endregion

        #region GetMapper test methods

        [Fact]
        public void RepositoryBaseGetMapper_ReturnValidMapper()
        {
            // Arrange
            var repository = BuildRepository();

            // Act
            var output = repository.GetMapper();

            // Assert
            output.Should().NotBeNull();
        }

        #endregion

        #region GetLogger test methods

        [Fact]
        public void RepositoryBaseGetLogger_ReturnValidLogger()
        {
            // Arrange
            var repository = BuildRepository();

            // Act
            var output = repository.GetLogger();

            // Assert
            output.Should().NotBeNull();
        }

        #endregion

        #region Private methods

        private FakeRepository BuildRepository()
        {
            var logger = _serviceProvider.GetService<ILogger<FakeRepository>>();

            var mapperMock = new Mock<IMapper>();

            var mongoCollectionMock = new Mock<IMongoCollection<FakeEntity>>();

            var mongoDatabaseMock = new Mock<IMongoDatabase>();
            mongoDatabaseMock.Setup(x => x.GetCollection<FakeEntity>("fake", null))
                .Returns(mongoCollectionMock.Object);

            var mongoDbContextMock = new Mock<IMongoDbContext>();
            mongoDbContextMock.Setup(x => x.GetDatabase(null))
                .Returns(mongoDatabaseMock.Object);

            return new FakeRepository(mongoDbContextMock.Object, logger, mapperMock.Object);
        }

        #endregion
    }
}
