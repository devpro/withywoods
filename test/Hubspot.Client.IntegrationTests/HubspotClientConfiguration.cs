using System;

namespace Withywoods.Hubspot.Client.IntegrationTests;

public class HubspotClientConfiguration(string environmentName) : IHubspotClientConfiguration
{
    public string HttpClientName => "Hubspot";

    public bool UseOAuth { get; init; }

    public string BaseUrl => GetEnvironmentVariable("BaseUrl");

    public string ApiKey => GetEnvironmentVariable("ApiKey");

    public string ApplicationId => GetEnvironmentVariable("ApplicationId");

    public string ClientId => GetEnvironmentVariable("ClientId");

    public string ClientSecret => GetEnvironmentVariable("ClientSecret");

    public string OAuthUrl => GetEnvironmentVariable("OAuthUrl");

    public string RedirectUrl => GetEnvironmentVariable("RedirectUrl");

    private string GetEnvironmentVariable(string name)
    {
        var variableName = $"Hubspot__{environmentName}__{name}";
        return Environment.GetEnvironmentVariable(variableName, EnvironmentVariableTarget.Process)
               ?? throw new InvalidOperationException($"Environment variable {variableName} was not found.");
    }
}
