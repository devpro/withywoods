using FluentAssertions;
using Withywoods.Dal.MongoDb.UnitTests.Fakes;
using Xunit;

namespace Withywoods.Dal.MongoDb.UnitTests
{
    [Trait("Category", "UnitTests")]
    public class MongoClientFactoryTest
    {
        [Fact]
        public void MongoClientFactoryCreateClient_ReturnValidClient()
        {
            // Arrange
            var configuration = new FakeConfiguration();
            var factory = new MongoClientFactory();

            // Act
            var output = factory.CreateClient(configuration.ConnectionString);

            // Assert
            output.Should().NotBeNull();
        }
    }
}
