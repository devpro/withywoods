using System.Net.Http;
using Microsoft.Extensions.Logging;
using Withywoods.Net.Http;

namespace Devpro.Yanport.Client.Repositories
{
    public abstract class RepositoryBase : HttpRepositoryBase
    {
        protected RepositoryBase(
            IYanportClientConfiguration configuration,
            ILogger logger,
            IHttpClientFactory httpClientFactory)
            : base(logger, httpClientFactory)
        {
            Configuration = configuration;
        }

        protected IYanportClientConfiguration Configuration { get; private set; }

        protected abstract string ResourceName { get; }

        protected override string HttpClientName => Configuration.HttpClientName;

        protected string GenerateUrl(string prefix = "", string suffix = "", string arguments = "")
        {
            return $"{Configuration.BaseUrl}{prefix}/{ResourceName}{suffix}{arguments}";
        }
    }
}
