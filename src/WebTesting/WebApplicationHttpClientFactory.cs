using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Withywoods.WebTesting
{
    /// <summary>
    /// Web Application http client factory to be able to use WebApplicationFactory as IHttpClientFactory.
    /// </summary>
    /// <typeparam name="TEntryPoint"></typeparam>
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
