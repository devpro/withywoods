using System;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Withywoods.Hubspot.Abstractions.Models;

public partial class TokenModel
{
    [JsonPropertyName("refresh_token")]
    public required string RefreshToken { get; set; }

    [JsonPropertyName("access_token")]
    public required string AccessToken { get; set; }

    /// <summary>
    /// The access token will expire after the number of seconds given in the expires_in field of the response.
    /// </summary>
    /// <example>21600 for 6 hours</example>
    [JsonPropertyName("expires_in")]
    public long ExpiresIn { get; set; }

    /// <summary>
    /// Expired DateTime (UTC).
    /// </summary>
    [IgnoreDataMember]
    public DateTime ExpiredAt { get; set; }

    /// <summary>
    /// Created DateTime (UTC).
    /// </summary>
    [IgnoreDataMember]
    public DateTime CreatedAt { get; set; }
}
