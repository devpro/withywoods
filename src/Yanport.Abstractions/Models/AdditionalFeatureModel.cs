using Newtonsoft.Json;

namespace Devpro.Yanport.Abstractions.Models
{
    public class AdditionalFeatureModel
    {
        public FeaturesModel Features { get; set; }
        [JsonProperty("type")]
        public string AdditionalFeatureType { get; set; }
    }
}
