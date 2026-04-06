using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Withywoods.Yanport.Abstractions.Models;

public class HitModel
{
    public string Id { get; set; } = string.Empty;

    public string Source { get; set; } = string.Empty;

    public MainFeaturesModel Features { get; set; } = new();

    public MarketingModel Marketing { get; set; } = new();

    public AddressModel Address { get; set; } = new();

    public List<AdModel> Ads { get; set; } = [];

    public List<object> Properties { get; set; } = [];

    [JsonPropertyName("type")]
    public string HitType { get; set; } = string.Empty;
}
