using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace Withywoods.WebTesting.Rest
{
    [Obsolete("This class will soon be removed, please use ResourceTestBase class of the same namespace instead.")]
    [ExcludeFromCodeCoverage]
    public class BasicRestRunner
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public BasicRestRunner(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<string> Post(string url, string body, HttpStatusCode httpStatusCode = HttpStatusCode.Created)
        {
            return await Post(url, new StringContent(body, Encoding.UTF8, "application/json"), httpStatusCode);
        }

        public async Task<string> Post(string url, HttpContent bodyContent, HttpStatusCode httpStatusCode = HttpStatusCode.Created)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.PostAsync(url, bodyContent);
            response.StatusCode.Should().Be(httpStatusCode);
            var stringResponse = await response.Content.ReadAsStringAsync();
            return stringResponse;
        }
    }
}
