using Newtonsoft.Json;

namespace Withywoods.Serialization.Json
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// Serialize an object in a Json string.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}
