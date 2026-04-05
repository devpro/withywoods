using System.Collections.Generic;
using Newtonsoft.Json;

namespace Devpro.Yanport.Abstractions.Models
{
    public class AreaModel
    {
        public List<object> KitchenEquipments { get; set; }
        [JsonProperty("type")]
        public string AreaType { get; set; }
    }
}
