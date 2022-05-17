using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Withywoods.WebTesting.Models;
using Withywoods.WebTesting.Rest;

namespace Withywoods.WebTesting.WorkFlows
{
    public class GenericApiFlow
    {
        private readonly RestClient _restClient;

        private readonly List<HttpRequestModel> _httpRequests = new();

        public GenericApiFlow(string baseAddress)
        {
            var httpClient = new HttpClient { BaseAddress = new Uri(baseAddress) };
            _restClient = new RestClient(httpClient);
        }

        public HttpRequestModel GivenTheRequest(string action, HttpMethod method)
        {
            var model = new HttpRequestModel(action, method);
            _httpRequests.Add(model);
            return model;
        }

        public async Task ExecuteAsync()
        {
            foreach (var request in _httpRequests)
            {
                await request.ExecuteAsync(_restClient);
            }
        }
    }
}
