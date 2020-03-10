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
        [Fact]
        public void AddTwohireClient_ShouldProvideChannelFactory()
        {
            // Arrange
            var fixture = new Fixture();
            var serviceCollection = new ServiceCollection();
            var configuration = fixture.Create<DefaultRabbitMqConfiguration>();

            // Act
            serviceCollection.AddRabbitMqFactory(configuration);

            // Assert
            var services = serviceCollection.BuildServiceProvider();
            var exc = Assert.Throws<BrokerUnreachableException>(() => services.GetRequiredService<IChannelFactory>());
            exc.Should().NotBeNull();
            exc.Message.Should().Be("None of the specified endpoints were reachable"); // no RabbitMQ is running in CI environment
        }

        [Fact]
        public void AddTwohireClient_ShouldProvideConnectionFactory()
        {
            // Arrange
            var fixture = new Fixture();
            var serviceCollection = new ServiceCollection();
            var configuration = fixture.Create<DefaultRabbitMqConfiguration>();

            // Act
            serviceCollection.AddRabbitMqFactory(configuration);

            // Assert
            var services = serviceCollection.BuildServiceProvider();
            var connectionFactory = services.GetRequiredService<IConnectionFactory>() as ConnectionFactory;
            connectionFactory.Should().NotBeNull();
            connectionFactory.HostName.Should().Be(configuration.Hostname);
            connectionFactory.Port.Should().Be(configuration.Port);
        }

        [Fact]
        public void AddTwohireClient_ShouldProvideConfiguration()
        {
            // Arrange
            var fixture = new Fixture();
            var serviceCollection = new ServiceCollection();
            var configuration = fixture.Create<DefaultRabbitMqConfiguration>();

            // Act
            serviceCollection.AddRabbitMqFactory(configuration);

            // Assert
            var services = serviceCollection.BuildServiceProvider();
            services.GetRequiredService<IRabbitMqConfiguration>().Should().NotBeNull();
            services.GetRequiredService<IRabbitMqConfiguration>().Should().BeEquivalentTo(configuration);
        }

        [Fact]
        public void AddTwohireClient_ShouldThrowExceptionIfServiceCollectionIsNull()
        {
            // Arrange
            var fixture = new Fixture();
            var serviceCollection = (ServiceCollection)null;
            var configuration = fixture.Create<DefaultRabbitMqConfiguration>();

            // Act
            var exc = Assert.Throws<ArgumentNullException>(() => serviceCollection.AddRabbitMqFactory(configuration));

            // Assert
            exc.Should().NotBeNull();
            exc.Message.Should().Be("Value cannot be null. (Parameter 'services')");
        }

        [Fact]
        public void AddTwohireClient_ShouldThrowExceptionIfConfigurationIsNull()
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
