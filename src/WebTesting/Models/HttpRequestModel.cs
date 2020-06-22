using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Withywoods.Serialization.Json;
using Withywoods.WebTesting.Rest;

namespace Withywoods.WebTesting.Models
{
    public class HttpRequestModel
    {
        private readonly string _action;

        private readonly HttpMethod _method;

        private string _body;

        private string _expectedContent;

        private bool _expectedContentNotEmptyContent;

        private HttpStatusCode? _expectedCode;

        public HttpRequestModel(string action, HttpMethod method)
        {
            _action = action;
            _method = method;
        }

        public HttpRequestModel WithBody(string body) { _body = body; return this; }

        public HttpRequestModel WithBody(object body) { return WithBody(body.ToJson()); }

        public HttpRequestModel WhenSentThenShouldReturnCode(HttpStatusCode statusCode) { _expectedCode = statusCode; return this; }

        public HttpRequestModel AndContentShouldBe(string expectedContent) { _expectedContent = expectedContent; return this; }

        public HttpRequestModel AndContentShouldNotBeEmpty() { _expectedContentNotEmptyContent = true; return this; }

        public async Task ExecuteAsync(RestClient restClient)
        {
            string output;
            if (_method == HttpMethod.Get)
            {
                output = await restClient.GetAsync(_action, _expectedCode ?? HttpStatusCode.OK);
            }
            else if (_method == HttpMethod.Post)
            {
                output = await restClient.PostAsync(_action, _body, _expectedCode ?? HttpStatusCode.Created);
            }
            else
            {
                throw new NotImplementedException();
            }

            if (!string.IsNullOrEmpty(_expectedContent))
            {
                output.Should().Be(_expectedContent);
            }

            if (_expectedContentNotEmptyContent)
            {
                output.Should().NotBeNullOrEmpty();
            }
        }
    }
}
