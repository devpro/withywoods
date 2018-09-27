using AutoMapper;
using MongoDB.Bson;

namespace Devpro.Withywoods.Dal.MongoDb.MappingConverters
{
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
        /// <returns></returns>
        public string Convert(ObjectId source, string destination, ResolutionContext context)
        {
            if (source == ObjectId.Empty)
                return null;
            return source.ToString();
        }
    }
}
