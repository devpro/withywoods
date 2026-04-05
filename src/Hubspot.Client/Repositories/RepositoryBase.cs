using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.Extensions.Logging;
using Withywoods.Hubspot.Abstractions.Providers;
using Withywoods.Net.Http;

namespace Withywoods.Hubspot.Client.Repositories;

public abstract class RepositoryBase(
    IHubspotClientConfiguration configuration,
    ILogger logger,
    IHttpClientFactory httpClientFactory,
    ITokenProvider? tokenProvider)
    : HttpRepositoryBase(logger, httpClientFactory)
{
    protected IHubspotClientConfiguration Configuration { get; } = configuration;

    protected abstract string ResourceName { get; }

    protected override string HttpClientName => Configuration.HttpClientName;

    protected string GenerateUrl(string prefix = "", string suffix = "", string arguments = "")
    {
        if (!Configuration.UseOAuth)
        {
            arguments = string.IsNullOrEmpty(arguments) ? $"?hapikey={Configuration.ApiKey}" : $"{arguments}&hapikey={Configuration.ApiKey}";
        }

        return $"{Configuration.BaseUrl}{prefix}/{ResourceName}{suffix}{arguments}";
    }

    protected override void EnrichRequestHeaders(HttpClient client)
    {
        if (Configuration.UseOAuth && tokenProvider != null)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenProvider.Token);
        }
    }
}
