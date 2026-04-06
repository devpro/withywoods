using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Withywoods.Hubspot.Abstractions.Models;

public partial class IdentityProfileModel
{
    public long Vid { get; set; }

    [JsonPropertyName("saved-at-timestamp")]
    public long SavedAtTimestamp { get; set; }

    [JsonPropertyName("deleted-changed-timestamp")]
    public long DeletedChangedTimestamp { get; set; }

    public List<IdentityModel> Identities { get; set; } = [];
}
