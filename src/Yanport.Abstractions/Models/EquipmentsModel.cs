using Newtonsoft.Json;

namespace Devpro.Yanport.Abstractions.Models
{
    public class EquipmentsModel
    {
        [JsonProperty("FURNITURE")]
        public bool Furniture { get; set; }
    }
}
