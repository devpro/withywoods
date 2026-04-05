using System;
using System.Text.Json.Serialization;

namespace Withywoods.Twohire.Client.Dto;

public class TokenDto
{
    public int Id { get; set; }

    public string Code { get; set; } = string.Empty;

    public long Expire { get; set; }

    public int ClientType { get; set; }

    public int UserId { get; set; }

    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }

    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt { get; set; }

    public UserDto User { get; set; } = new();

    public bool Unlimited { get; set; }
}
