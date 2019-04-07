using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using Moq;
using Withywoods.Dal.MongoDb.Serialization;
using Withywoods.Dal.MongoDb.UnitTests.Fakes;
using Xunit;

namespace Withywoods.Dal.MongoDb.UnitTests
{
    [Trait("Category", "UnitTests")]
    public class DefaultMongoDbContextTest
    {
        public DefaultMongoDbContextTest()
        {
            CleanConventionsInRegistry();
        }

        #region GetDatabase test methods

        [Fact]
        public void DefaultMongoDbContextGetDatabase_WhenAllConventionsSet_ReturnValidDatabase()
        {
            // Arrange
            var serializationConventions = new List<string>
            {
                ConventionValues.CamelCaseElementName,
                ConventionValues.EnumAsString,
                ConventionValues.IgnoreExtraElements,
                ConventionValues.IgnoreNullValues,
                "DummyNotExisting"
            };
            var mongoDatabaseMock = new Mock<IMongoDatabase>();
            var mongoDbContext = BuildMongoDbContext(serializationConventions,
                (mock) =>
                {
                    mock.Setup(x => x.GetDatabase("fakeDb", null))
                        .Returns(mongoDatabaseMock.Object);
                });

            // Act
            var output = mongoDbContext.GetDatabase();

            // Assert
            output.Should().NotBeNull();
            string.Join(',', GetConventionsInRegistry()).Should().Be("Attribute,CamelCaseElementName,EnumRepresentation,IgnoreExtraElements,IgnoreExtraElements,"
                + "IgnoreIfNull,ImmutableTypeClassMap,LookupIdGenerator,NamedExtraElementsMember,NamedIdMember,NamedParameterCreatorMap,"
                + "ReadWriteMemberFinder,StringObjectIdIdGenerator");
        }

        [Fact]
        public void DefaultMongoDbContextGetDatabase_WhenNoConventionsSet_ReturnValidDatabase()
        {
            // Arrange
            var serializationConventions = new List<string>();
            var mongoDatabaseMock = new Mock<IMongoDatabase>();
            var mongoDbContext = BuildMongoDbContext(serializationConventions,
                (mock) =>
                {
                    mock.Setup(x => x.GetDatabase("anotherDb", null))
                        .Returns(mongoDatabaseMock.Object);
                });

            // Act
            var output = mongoDbContext.GetDatabase("anotherDb");

            // Assert
            output.Should().NotBeNull();
            string.Join(',', GetConventionsInRegistry()).Should().Be("Attribute,IgnoreExtraElements,ImmutableTypeClassMap,LookupIdGenerator,"
                + "NamedExtraElementsMember,NamedIdMember,NamedParameterCreatorMap,ReadWriteMemberFinder,StringObjectIdIdGenerator");
        }

        [Fact]
        public void DefaultMongoDbContextGetDatabase_WhenIncompleteConfiguration_ReturnValidDatabase()
        {
            // Arrange
            List<string> serializationConventions = null;
            var mongoDatabaseMock = new Mock<IMongoDatabase>();
            var mongoDbContext = BuildMongoDbContext(serializationConventions,
                (mock) =>
                {
                    mock.Setup(x => x.GetDatabase("anotherDb", null))
                        .Returns(mongoDatabaseMock.Object);
                });

            // Act
            var output = mongoDbContext.GetDatabase("anotherDb");

            // Assert
            output.Should().NotBeNull();
            string.Join(',', GetConventionsInRegistry()).Should().Be("Attribute,IgnoreExtraElements,ImmutableTypeClassMap,LookupIdGenerator,"
                + "NamedExtraElementsMember,NamedIdMember,NamedParameterCreatorMap,ReadWriteMemberFinder,StringObjectIdIdGenerator");
        }

        #endregion

        #region Private methods

        private DefaultMongoDbContext BuildMongoDbContext(List<string> serializationConventions, Action<Mock<IMongoClient>> updateMock = null)
        {
            var configuration = new FakeConfiguration
            {
                SerializationConventions = serializationConventions
            };

            var mongoClientMock = new Mock<IMongoClient>();
            updateMock?.Invoke(mongoClientMock);

            var factoryMock = new Mock<IMongoClientFactory>();
            factoryMock.Setup(x => x.CreateClient(configuration.ConnectionString))
                .Returns(mongoClientMock.Object);

            return new DefaultMongoDbContext(configuration, factoryMock.Object);
        }

        private void CleanConventionsInRegistry()
        {
            ConventionRegistry.Remove(ConventionValues.CamelCaseElementName);
            ConventionRegistry.Remove(ConventionValues.EnumAsString);
            ConventionRegistry.Remove(ConventionValues.IgnoreExtraElements);
            ConventionRegistry.Remove(ConventionValues.IgnoreNullValues);
        }

        private List<string> GetConventionsInRegistry()
        {
            return ConventionRegistry.Lookup(typeof(object)).Conventions
                .OrderBy(x => x.Name)
                .Select(x => x.Name)
                .ToList();
        }

        #endregion
    }
}
