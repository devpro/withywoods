using System.Text.Json.Serialization;

namespace Withywoods.Yanport.Abstractions.Models;

public class EquipmentsModel
{
    [JsonPropertyName("FURNITURE")]
    public bool Furniture { get; set; }
}
