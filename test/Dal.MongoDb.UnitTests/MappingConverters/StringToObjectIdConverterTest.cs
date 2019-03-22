using AutoMapper;
using FluentAssertions;
using MongoDB.Bson;
using Withywoods.Dal.MongoDb.MappingConverters;
using Xunit;

namespace Withywoods.Dal.MongoDb.UnitTests.MappingConverters
{
    [Trait("Category", "UnitTests")]
    public class StringToObjectIdConverterTest
    {
        private readonly IMapper _mapper;

        public StringToObjectIdConverterTest()
        {
            var config = new MapperConfiguration(x =>
            {
                x.CreateMap<string, ObjectId>()
                    .ConvertUsing<StringToObjectIdConverter>();
                x.AllowNullCollections = true;
            });
            _mapper = config.CreateMapper();
        }

        [Fact]
        public void StringToObjectIdConverterAssertConfigurationIsValid_DoNotThrowAnException()
        {
            _mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }

        [Fact]
        public void StringToObjectIdConverterMap_WhenValidObjectId_ReturnValidObjectId()
        {
            var output = _mapper.Map<ObjectId>("5ba893a670f3b02e3d34e24c");
            output.Should().BeEquivalentTo(new ObjectId("5ba893a670f3b02e3d34e24c"));
        }

        [Fact]
        public void StringToObjectIdConverterMap_WhenInvalidObjectId_ReturnEmptyObjectId()
        {
            var output = _mapper.Map<ObjectId>("1234");
            output.Should().BeEquivalentTo(ObjectId.Empty);
        }

        [Fact]
        public void StringToObjectIdConverterMap_WhenNull_ReturnEmptyObjectId()
        {
            var output = _mapper.Map<ObjectId>(null);
            output.Should().BeEquivalentTo(ObjectId.Empty);
        }

        [Fact]
        public void StringToObjectIdConverterMap_WhenEmpty_ReturnEmptyObjectId()
        {
            var output = _mapper.Map<ObjectId>(string.Empty);
            output.Should().BeEquivalentTo(ObjectId.Empty);
        }
    }
}
