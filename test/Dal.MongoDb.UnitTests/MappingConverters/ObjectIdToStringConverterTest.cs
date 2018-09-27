using AutoMapper;
using Devpro.Withywoods.Dal.MongoDb.MappingConverters;
using FluentAssertions;
using MongoDB.Bson;
using Xunit;

namespace Devpro.Withywoods.Dal.MongoDb.UnitTests.MappingConverters
{
    public class ObjectIdToStringConverterTest
    {
        private readonly IMapper _mapper;

        public ObjectIdToStringConverterTest()
        {
            var config = new MapperConfiguration(x =>
            {
                x.CreateMap<ObjectId, string>()
                    .ConvertUsing<ObjectIdToStringConverter>();
                x.AllowNullCollections = true;
            });
            _mapper = config.CreateMapper();
        }

        [Fact]
        public void AssertConfigurationIsValid_WhenDefault_ThenDoNotThrowException()
        {
            _mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }

        [Fact]
        public void Map_WhenValidObjectIdString_ThenReturnValidString()
        {
            var output = _mapper.Map<string>(new ObjectId("5ba893a670f3b02e3d34e24c"));
            output.Should().Be("5ba893a670f3b02e3d34e24c");
        }

        [Fact]
        public void Map_WhenEmptyString_ThenReturnNull()
        {
            var output = _mapper.Map<string>(ObjectId.Empty);
            output.Should().BeNull();
        }
    }
}
