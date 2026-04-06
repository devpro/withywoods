using System.Net.Http;
using Microsoft.Extensions.Logging;
using Withywoods.Net.Http;

namespace Withywoods.Yanport.Client.Repositories;

public abstract class RepositoryBase(
    IYanportClientConfiguration configuration,
    ILogger logger,
    IHttpClientFactory httpClientFactory)
    : HttpRepositoryBase(logger, httpClientFactory)
{
    protected abstract string ResourceName { get; }

    protected override string HttpClientName => configuration.HttpClientName;

    protected string GenerateUrl(string prefix = "", string suffix = "", string arguments = "")
    {
        return $"{configuration.BaseUrl}{prefix}/{ResourceName}{suffix}{arguments}";
    }
}
