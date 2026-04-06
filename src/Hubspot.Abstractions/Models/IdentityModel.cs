namespace Withywoods.Hubspot.Abstractions.Models;

public partial class IdentityModel
{
    public string Type { get; set; } = string.Empty;

    public string Value { get; set; } = string.Empty;

    public long Timestamp { get; set; }
}
