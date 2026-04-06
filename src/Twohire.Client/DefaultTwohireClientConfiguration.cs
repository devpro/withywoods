namespace Withywoods.Twohire.Client;

public class DefaultTwohireClientConfiguration : ITwohireClientConfiguration
{
    public string HttpClientName { get; set; } = "Twohire";

    public required string BaseUrl { get; set; }

    public required string Version { get; set; }

    public required string ServiceToken { get; set; }

    public required string Username { get; set; }

    public required string Password { get; set; }
}
