using AutoMapper;
using FluentAssertions;
using MongoDB.Bson;
using Withywoods.Dal.MongoDb.MappingConverters;
using Xunit;

namespace Withywoods.Dal.MongoDb.UnitTests.MappingConverters
{
    [Trait("Category", "UnitTests")]
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
        public void ObjectIdToStringConverterAssertConfigurationIsValid_DoNotThrowAnException()
        {
            _mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }

        [Fact]
        public void ObjectIdToStringConverterMap_WhenValidObjectIdString_ReturnValidString()
        {
            var output = _mapper.Map<string>(new ObjectId("5ba893a670f3b02e3d34e24c"));
            output.Should().Be("5ba893a670f3b02e3d34e24c");
        }

        [Fact]
        public void ObjectIdToStringConverterMap_WhenEmptyString_ReturnNull()
        {
            var output = _mapper.Map<string>(ObjectId.Empty);
            output.Should().BeNull();
        }
    }
}
