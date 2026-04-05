using Newtonsoft.Json;

namespace Devpro.Yanport.Abstractions.Models
{
    public class AreaCountModel
    {
        [JsonProperty("BEDROOM")]
        public int Bedroom { get; set; }

        [JsonProperty("GARDEN")]
        public int Garden { get; set; }

        [JsonProperty("PARKING")]
        public int Parking { get; set; }

        [JsonProperty("TERRACE")]
        public int Terrace { get; set; }
    }
}
