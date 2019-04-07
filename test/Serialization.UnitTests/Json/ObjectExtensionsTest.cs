using System;
using FluentAssertions;
using Withywoods.Serialization.Json;
using Withywoods.Serialization.UnitTests.Fakes;
using Xunit;

namespace Withywoods.Serialization.UnitTests.Json
{
    [Trait("Category", "UnitTests")]
    public class ObjectExtensionsTest
    {
        [Fact]
        public void SerializationJsonObjectExtensionToJson_ReturnValidJsonString()
        {
            // Arrange
            var basicDto = new FakeBasicDto
            {
                Id = "642de9e8-a61e-4137-ab44-95fe7e9ecc1f",
                CreatedAt = new DateTime(1998, 07, 12, 22, 05, 30),
                Name = "Première étoile",
                Score = 3.0
            };

            // Act
            var output = basicDto.ToJson();

            // Assert
            output.Should().Be("{\"Id\":\"642de9e8-a61e-4137-ab44-95fe7e9ecc1f\",\"Name\":\"Première étoile\",\"Score\":3.0,\"CreatedAt\":\"1998-07-12T22:05:30\"}");
        }
    }
}
