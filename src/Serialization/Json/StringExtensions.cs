using Newtonsoft.Json;

namespace Withywoods.Serialization.Json
{
    public static class StringExtensions
    {
        /// <summary>
        /// Deserialize a json string into an object of type <see cref="T"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T FromJson<T>(this string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
