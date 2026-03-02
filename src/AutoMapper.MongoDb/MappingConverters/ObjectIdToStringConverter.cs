using AutoMapper;
using MongoDB.Bson;

namespace Withywoods.AutoMapper.MongoDb.MappingConverters;

/// <summary>
/// AutoMapper type Converter from <see cref="ObjectId"/> to <see cref="string"/>.
/// </summary>
public class ObjectIdToStringConverter : ITypeConverter<ObjectId, string>
{
    /// <summary>
    /// Convert source to destination using context.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="destination"></param>
    /// <param name="context"></param>
    /// <returns>String value of the object id, null if the object id is empty</returns>
    public string Convert(ObjectId source, string destination, ResolutionContext context)
    {
        return source == ObjectId.Empty ? string.Empty : source.ToString();
    }
}
