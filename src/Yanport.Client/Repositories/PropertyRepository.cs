using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Devpro.Yanport.Abstractions.Models;
using Devpro.Yanport.Abstractions.Repositories;
using Microsoft.Extensions.Logging;
using Withywoods.Net.Http.Exceptions;
using Withywoods.Serialization.Json;

namespace Devpro.Yanport.Client.Repositories
{
    public class PropertyRepository : RepositoryBase, IPropertyRepository
    {
        public PropertyRepository(IYanportClientConfiguration configuration, ILogger<PropertyRepository> logger, IHttpClientFactory httpClientFactory)
            : base(configuration, logger, httpClientFactory)
        {
        }

        protected override string ResourceName => "properties";

        public async Task<List<HitModel>> FindAllAsync(string queryParameters = null)
        {
            var url = GenerateUrl(arguments: queryParameters);
            var output = await GetAsync<ResultModel>(url);
            return output.Hits;
        }

        protected override async Task<T> GetAsync<T>(string url) where T : class
        {
            var client = string.IsNullOrEmpty(HttpClientName) ? HttpClientFactory.CreateClient()
                : HttpClientFactory.CreateClient(HttpClientName);

            var response = await client.GetAsync(url);

            var stringResult = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                Logger.LogDebug($"Status code doesn't indicate success [HttpRequestUrl={url}] [HttpResponseStatusCode={response.StatusCode}] [HttpResponseContent={stringResult}]");
                throw new ConnectivityException($"The response status \"{response.StatusCode}\" is not a success (reason=\"{response.ReasonPhrase}\")");
            }

            try
            {
                return stringResult.FromJson<T>();
            }
            catch (Exception exc)
            {
                Logger.LogWarning($"Cannot deserialize GET call response content [HttpRequestUrl={url}] [SerializationType={typeof(T)}] [ExceptionMessage={exc.Message}]");
                Logger.LogDebug($"[HttpResponseContent={stringResult}]");
                Logger.LogDebug($"[Stacktrace={exc.StackTrace}]");
                throw new ConnectivityException($"Invalid data received when calling \"{url}\". {exc.Message}.", exc);
            }
        }
    }
}
