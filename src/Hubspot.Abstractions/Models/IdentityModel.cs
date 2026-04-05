namespace Withywoods.Hubspot.Abstractions.Models;

public partial class IdentityModel
{
    public required string Type { get; set; }

    public required string Value { get; set; }

    public long Timestamp { get; set; }
}
