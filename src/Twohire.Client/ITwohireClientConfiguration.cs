namespace Withywoods.Twohire.Client;

/// <summary>
/// 2hire client configuration.
/// </summary>
public interface ITwohireClientConfiguration
{
    /// <summary>
    /// 2hire REST Api base URL.
    /// </summary>
    public string BaseUrl { get; }

    /// <summary>
    /// 2hire REST API version.
    /// </summary>
    public string Version { get; }

    /// <summary>
    /// 2hire REST API service token.
    /// </summary>
    public string ServiceToken { get; }

    /// <summary>
    /// 2hire REST API username.
    /// </summary>
    public string Username { get; }

    /// <summary>
    /// 2hire REST API password.
    /// </summary>
    public string Password { get; }

    /// <summary>
    /// HTTP client name.
    /// This is a free text but enables the application using this library to make sure there is no conflict with other HTTP client registration.
    /// </summary>
    public string HttpClientName { get; }
}
