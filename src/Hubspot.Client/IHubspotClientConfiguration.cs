namespace Withywoods.Hubspot.Client;

/// <summary>
/// Client library configuration.
/// It's up to the application that uses this library to decide where to store and read the configuration values.
/// </summary>
public interface IHubspotClientConfiguration
{
    /// <summary>
    /// Hubspot base URL.
    /// </summary>
    public string BaseUrl { get; }

    /// <summary>
    /// Use OAuth to authenticate HubSpot API requests?
    /// If true, the following fields must be defined: ApplicationId, ClientId, ClientSecret, RedirectUrl.
    /// If false, the following field must be defined: ApiKey.
    /// </summary>
    public bool UseOAuth { get; }

    /// <summary>
    /// HubSpot API key.
    /// </summary>
    public string ApiKey { get; }

    /// <summary>
    /// HubSpot Application ID.
    /// </summary>
    public string ApplicationId { get; }

    /// <summary>
    /// HubSpot Client ID.
    /// </summary>
    public string ClientId { get; }

    /// <summary>
    /// HubSpot Client Secret.
    /// </summary>
    public string ClientSecret { get; }

    /// <summary>
    /// HubSpot Application Redirect URL.
    /// </summary>
    public string RedirectUrl { get; }

    /// <summary>
    /// HTTP client name, this is a pure technical name, free text only used internally in the application.
    /// Make sure it's unique to prevent any conflict with another HTTP Client.
    /// </summary>
    public string HttpClientName { get; }
}
