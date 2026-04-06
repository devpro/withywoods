using System;

namespace Withywoods.Twohire.Client.IntegrationTests;

public class TwoHireRestApiConfiguration(string environmentName) : ITwohireClientConfiguration
{
    public string HttpClientName => "TwoHire";

    public string BaseUrl => GetEnvironmentVariable("BaseUrl");

    public string Version => GetEnvironmentVariable("ApiVersion");

    public string ServiceToken => GetEnvironmentVariable("ServiceToken");

    public string Username => GetEnvironmentVariable("Username");

    public string Password => GetEnvironmentVariable("Password");

    private string GetEnvironmentVariable(string name)
    {
        var variableName = $"Hubspot__{environmentName}__{name}";
        return Environment.GetEnvironmentVariable(variableName, EnvironmentVariableTarget.Process)
               ?? throw new InvalidOperationException($"Environment variable {variableName} was not found.");
    }
}
