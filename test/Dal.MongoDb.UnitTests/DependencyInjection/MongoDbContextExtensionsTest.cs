using System;
using Devpro.Withywoods.Dal.MongoDb.DependencyInjection;
using Devpro.Withywoods.Dal.MongoDb.UnitTests.Fakes;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Devpro.Withywoods.Dal.MongoDb.UnitTests.DependencyInjection
{
    [Trait("Category", "UnitTests")]
    public class MongoDbContextExtensionsTest
    {
        /// <summary>
        /// Verifies that AddMongoDbContext is enough to create a db context and get the database.
        /// </summary>
        [Fact]
        public void AddMongoDbContext_IsSelfContained_CanGetDatabase()
        {
            // Arrange
            var serviceCollection = new ServiceCollection();

            // Act
            serviceCollection.AddMongoDbContext<FakeConfiguration>();
            var services = serviceCollection.BuildServiceProvider();
            var mongoDbContext = services.GetRequiredService<IMongoDbContext>();
            var database = mongoDbContext.GetDatabase();

            // Assert
            database.Should().NotBeNull();
        }

        /// <summary>
        /// Verifies the error sent when no services are provided.
        /// </summary>
        [Fact]
        public void AddMongoDbContext_WhenNoServices_ThrowAnError()
        {
            // Arrange
            ServiceCollection serviceCollection = null;

            // Act
            var exc = Assert.Throws<ArgumentNullException>(() => serviceCollection.AddMongoDbContext<FakeConfiguration>());

            // Assert
            exc.Should().NotBeNull();
        }
    }
}
