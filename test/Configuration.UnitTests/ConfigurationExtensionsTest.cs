using System;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace Withywoods.Configuration.UnitTests
{
    [Trait("Category", "UnitTests")]

    public class ConfigurationExtensionsTest
    {
        [Fact]
        public void ConfigurationExtensionGetSection_WhenSectionExists_ReturnData()
        {
            // Arrange
            var configurationSectionMock = new Mock<IConfigurationSection>();
            var configurationMock = new Mock<IConfiguration>();
            configurationMock.Setup(x => x.GetSection("dummy"))
                .Returns(configurationSectionMock.Object);

            // Act
            var output = configurationMock.Object.TryGetSection("dummy");

            // Assert
            output.Should().Be(configurationSectionMock.Object);
        }

        [Fact]
        public void ConfigurationExtensionGetSection_WhenSectionDoesNotExist_ThrowException()
        {
            // Arrange
            var configurationMock = new Mock<IConfiguration>();

            // Act
            var exc = Assert.Throws<ArgumentException>(() => configurationMock.Object.TryGetSection("dummy"));

            // Assert
            exc.Should().NotBeNull();
            exc.Message.Should().Be("Missing section \"dummy\" in configuration");
        }
    }
}
