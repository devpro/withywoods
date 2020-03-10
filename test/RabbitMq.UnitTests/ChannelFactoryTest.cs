using FluentAssertions;
using Moq;
using RabbitMQ.Client;
using Xunit;

namespace Withywoods.RabbitMq.UnitTests
{
    [Trait("Category", "UnitTests")]
    public class ChannelFactoryTest
    {
        [Fact]
        public void RabbitMqChannelFactoryCreate_ShouldCreateRabbitMqModel()
        {
            // Arrange
            using var channelFactory = BuildChannelFactory(out var modelMock);

            // Act
            using var channel = channelFactory.Create();

            // Assert
            channel.Should().NotBeNull();
            channel.Should().BeEquivalentTo(modelMock.Object);
        }

        private IChannelFactory BuildChannelFactory(out Mock<IModel> modelMock)
        {
            modelMock = new Mock<IModel>();

            var connectionMock = new Mock<IConnection>();
            connectionMock.Setup(x => x.CreateModel())
                .Returns(modelMock.Object);

            var connectionFactoryMock = new Mock<IConnectionFactory>();
            connectionFactoryMock.Setup(x => x.CreateConnection())
                .Returns(connectionMock.Object);

            return new ChannelFactory(connectionFactoryMock.Object);
        }
    }
}
