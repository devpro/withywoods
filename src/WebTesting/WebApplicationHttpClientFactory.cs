using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Withywoods.WebTesting
{
    public class WebApplicationHttpClientFactory<TEntryPoint> : IHttpClientFactory where TEntryPoint : class
    {
        private readonly WebApplicationFactory<TEntryPoint> _webApplicationFactory;

        public WebApplicationHttpClientFactory(WebApplicationFactory<TEntryPoint> webApplicationFactory)
        {
            _webApplicationFactory = webApplicationFactory;
        }

        public HttpClient CreateClient(string name)
        {
            return _webApplicationFactory.CreateClient();
        }
    }
}
