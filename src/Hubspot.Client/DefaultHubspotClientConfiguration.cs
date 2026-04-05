namespace Withywoods.Hubspot.Client;

public class DefaultHubspotClientConfiguration : IHubspotClientConfiguration
{
    public required string BaseUrl { get; set; }

    public bool UseOAuth { get; set; }

    public required string ApiKey { get; set; }

    public required string ApplicationId { get; set; }

    public required string ClientId { get; set; }

    public required string ClientSecret { get; set; }

    public required string RedirectUrl { get; set; }

    public string HttpClientName { get; set; } = "Hubspot";
}
