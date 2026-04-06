using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Withywoods.Yanport.Abstractions.Models;

public class AreaModel
{
    public List<object> KitchenEquipments { get; set; } = [];

    [JsonPropertyName("type")]
    public string AreaType { get; set; } = string.Empty;
}
