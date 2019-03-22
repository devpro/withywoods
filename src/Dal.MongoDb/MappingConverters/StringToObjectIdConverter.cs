using AutoMapper;
using MongoDB.Bson;

namespace Withywoods.Dal.MongoDb.MappingConverters
{
    /// <summary>
    /// AutoMapper type Converter from <see cref="string"/> to <see cref="ObjectId"/>.
    /// </summary>
    public class StringToObjectIdConverter : ITypeConverter<string, ObjectId>
    {
        /// <summary>
        /// Convert source to destination using context.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="context"></param>
        /// <returns>The object id taken from parking the string, empty if the string is null or is not a correct object id value</returns>
        public ObjectId Convert(string source, ObjectId destination, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source)
             || !ObjectId.TryParse(source, out ObjectId output))
                return ObjectId.Empty;

            return output;
        }
    }
}
