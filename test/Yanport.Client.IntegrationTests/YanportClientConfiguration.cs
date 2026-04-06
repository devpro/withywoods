using System;

namespace Withywoods.Yanport.Client.IntegrationTests;

public class YanportClientConfiguration(string environmentName)
    : IYanportClientConfiguration
{
    public string HttpClientName => "Yanport";

    public string BaseUrl => GetEnvironmentVariable("BaseUrl");

    public string Token => GetEnvironmentVariable("Token");

    private string GetEnvironmentVariable(string name)
    {
        var variableName = $"Hubspot__{environmentName}__{name}";
        return Environment.GetEnvironmentVariable(variableName, EnvironmentVariableTarget.Process)
               ?? throw new InvalidOperationException($"Environment variable {variableName} was not found.");
    }
}
