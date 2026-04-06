using System.Text.Json.Serialization;

namespace Withywoods.Yanport.Abstractions.Models;

public class AdditionalFeatureModel
{
    public FeaturesModel Features { get; set; } = new();

    [JsonPropertyName("type")]
    public string AdditionalFeatureType { get; set; } = string.Empty;
}
