using System;
using FluentAssertions;
using Withywoods.Serialization.Json;
using Withywoods.Serialization.UnitTests.Fakes;
using Xunit;

namespace Withywoods.Serialization.UnitTests.Json
{
    [Trait("Category", "UnitTests")]
    public class StringExtensionsTest
    {
        [Fact]
        public void SerializationJsonStringExtensionFromJson_ReturnValidDtoObject()
        {
            // Arrange
            var jsonString = "{\"Id\":\"a2615387-a2cf-4a0d-bc85-9c06bc69941d\",\"Name\":\"Deuxième étoile\",\"Score\":4.2,\"CreatedAt\":\"2018-07-15T22:15:10\"}";

            // Act
            var output = jsonString.FromJson<FakeBasicDto>();

            // Assert
            output.Should().BeEquivalentTo(new FakeBasicDto
            {
                Id = "a2615387-a2cf-4a0d-bc85-9c06bc69941d",
                CreatedAt = new DateTime(2018, 07, 15, 22, 15, 10),
                Name = "Deuxième étoile",
                Score = 4.2
            });
        }
    }
}
