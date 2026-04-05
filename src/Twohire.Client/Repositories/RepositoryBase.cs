using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.Extensions.Logging;
using Withywoods.Net.Http;
using Withywoods.Twohire.Abstractions.Providers;

namespace Withywoods.Twohire.Client.Repositories
{
    public abstract class RepositoryBase(
        ITwohireClientConfiguration configuration,
        ILogger logger,
        IHttpClientFactory httpClientFactory,
        ITokenProvider? tokenProvider)
        : HttpRepositoryBase(logger, httpClientFactory)
    {
        protected ITwohireClientConfiguration Configuration { get; } = configuration;

        protected abstract string ResourceName { get; }

        protected override string HttpClientName => Configuration.HttpClientName;

        protected string GenerateUrl(string arguments = "")
        {
            return $"{Configuration.BaseUrl}/{Configuration.Version}/{ResourceName}{arguments}";
        }

        protected override void EnrichRequestHeaders(HttpClient client)
        {
            client.DefaultRequestHeaders.Add("x-service-token", Configuration.ServiceToken);
            if (tokenProvider?.Token != null)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenProvider.Token);
            }
        }
    }
}
