using System.Text.Json.Serialization;

namespace Withywoods.Yanport.Abstractions.Models;

public class AreaCountModel
{
    [JsonPropertyName("BEDROOM")]
    public int Bedroom { get; set; }

    [JsonPropertyName("GARDEN")]
    public int Garden { get; set; }

    [JsonPropertyName("PARKING")]
    public int Parking { get; set; }

    [JsonPropertyName("TERRACE")]
    public int Terrace { get; set; }
}
