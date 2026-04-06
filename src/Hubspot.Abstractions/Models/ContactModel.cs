using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Withywoods.Hubspot.Abstractions.Models;

public partial class ContactModel
{
    public long AddedAt { get; set; }

    public long Vid { get; set; }

    [JsonPropertyName("canonical-vid")]
    public long CanonicalVid { get; set; }

    [JsonPropertyName("merged-vids")]
    public List<object>? MergedVids { get; set; }

    [JsonPropertyName("portal-id")]
    public long PortalId { get; set; }

    [JsonPropertyName("is-contact")]
    public bool IsContact { get; set; }

    [JsonPropertyName("profile-token")]
    public string? ProfileToken { get; set; }

    [JsonPropertyName("profile-url")]
    public string? ProfileUrl { get; set; }

    public PropertiesModel? Properties { get; set; }

    [JsonPropertyName("form-submissions")]
    public List<object>? FormSubmissions { get; set; }

    [JsonPropertyName("identity-profiles")]
    public List<IdentityProfileModel>? IdentityProfiles { get; set; }

    [JsonPropertyName("merge-audits")]
    public List<object>? MergeAudits { get; set; }
}
