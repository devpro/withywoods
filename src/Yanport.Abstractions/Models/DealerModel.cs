using System.Text.Json.Serialization;

namespace Withywoods.Yanport.Abstractions.Models;

public class DealerModel
{
    public string Name { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public long Id { get; set; }

    public string SubType { get; set; } = string.Empty;

    public BloctelModel Bloctel { get; set; } = new();

    public bool AgenciesUnwanted { get; set; }

    [JsonPropertyName("type")]
    public string DealerType { get; set; } = string.Empty;
}
