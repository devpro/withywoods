using System;
using AutoFixture;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using Withywoods.RabbitMq.DependencyInjection;
using Xunit;

namespace Withywoods.RabbitMq.UnitTests.DependencyInjection
{
    [Trait("Category", "UnitTests")]
    public class ServiceCollectionExtensionsTest
    {
        private readonly Fixture _fixture;

        public ServiceCollectionExtensionsTest()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void AddRabbitMqFactory_WithUriAndRequestedHeartbeatAndContinuationTimeout_ShouldProvideConfiguration()
        {
            // Arrange
            var serviceCollection = new ServiceCollection();
            var configuration = new DefaultRabbitMqConfiguration
            {
                Uri = new Uri("amqp://localhost:5672/%2F"),
                RequestedHeartbeat = 30,
                ContinuationTimeout = new TimeSpan(1, 0, 0)
            };

            // Act
            serviceCollection.AddRabbitMqFactory(configuration);

            // Assert
            var services = serviceCollection.BuildServiceProvider();
            services.GetRequiredService<IRabbitMqConfiguration>().Should().NotBeNull();
            services.GetRequiredService<IRabbitMqConfiguration>().Should().BeEquivalentTo(configuration);
            var connectionFactory = services.GetRequiredService<IConnectionFactory>() as ConnectionFactory;
            connectionFactory.Should().NotBeNull();
            connectionFactory.Uri.Should().Be(configuration.Uri);
            connectionFactory.RequestedHeartbeat.Should().Be(configuration.RequestedHeartbeat.Value);
            connectionFactory.ContinuationTimeout.Should().Be(configuration.ContinuationTimeout.Value);
        }

        [Fact]
        public void AddRabbitMqFactory_WithHotnameOnly_ShouldProvideConnectionFactory()
        {
            // Arrange
            var serviceCollection = new ServiceCollection();
            var configuration = new DefaultRabbitMqConfiguration
            {
                Hostname = "localhost"
            };

            // Act
            serviceCollection.AddRabbitMqFactory(configuration);

            // Assert
            var services = serviceCollection.BuildServiceProvider();
            var connectionFactory = services.GetRequiredService<IConnectionFactory>() as ConnectionFactory;
            connectionFactory.Should().NotBeNull();
            connectionFactory.HostName.Should().Be(configuration.Hostname);
            connectionFactory.Port.Should().Be(5672);
        }

        [Fact]
        public void AddRabbitMqFactory_WithInvalidHostnameAndPort_ShouldProvideChannelFactory()
        {
            // Arrange
            var serviceCollection = new ServiceCollection();
            var configuration = new DefaultRabbitMqConfiguration
            {
                Hostname = "does.not.exist",
                Port = 1234
            };

            // Act
            serviceCollection.AddRabbitMqFactory(configuration);

            // Assert
            var services = serviceCollection.BuildServiceProvider();
            var connectionFactory = services.GetRequiredService<IConnectionFactory>() as ConnectionFactory;
            connectionFactory.Should().NotBeNull();
            connectionFactory.HostName.Should().Be(configuration.Hostname);
            connectionFactory.Port.Should().Be(configuration.Port);
            var exc = Assert.Throws<BrokerUnreachableException>(() => services.GetRequiredService<IChannelFactory>());
            exc.Should().NotBeNull();
            exc.Message.Should().Be("None of the specified endpoints were reachable"); // no RabbitMQ is running in CI environment
        }

        [Fact]
        public void AddRabbitMqFactory_ShouldThrowExceptionIfServiceCollectionIsNull()
        {
            // Arrange
            var serviceCollection = (ServiceCollection)null;
            var configuration = _fixture.Create<DefaultRabbitMqConfiguration>();

            // Act
            var exc = Assert.Throws<ArgumentNullException>(() => serviceCollection.AddRabbitMqFactory(configuration));

            // Assert
            exc.Should().NotBeNull();
            exc.Message.Should().Be("Value cannot be null. (Parameter 'services')");
        }

        [Fact]
        public void AddRabbitMqFactory_ShouldThrowExceptionIfConfigurationIsNull()
        {
            // Arrange
            var serviceCollection = new ServiceCollection();
            var configuration = (IRabbitMqConfiguration)null;

            // Act
            var exc = Assert.Throws<ArgumentNullException>(() => serviceCollection.AddRabbitMqFactory(configuration));

            // Assert
            exc.Should().NotBeNull();
            exc.Message.Should().Be("Value cannot be null. (Parameter 'configuration')");
        }
    }
}
