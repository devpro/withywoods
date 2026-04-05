using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Withywoods.Hubspot.Abstractions.Models;

public partial class ContactResultModel
{
    public List<ContactModel>? Contacts { get; set; }

    [JsonPropertyName("has-more")]
    public bool HasMore { get; set; }

    [JsonPropertyName("vid-offset")]
    public long VidOffset { get; set; }
}
