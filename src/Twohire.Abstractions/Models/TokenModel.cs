using System;

namespace Withywoods.Twohire.Abstractions.Models;

public class TokenModel
{
    public string Value { get; set; } = string.Empty;

    public DateTime ExpiredDate { get; set; }
}
