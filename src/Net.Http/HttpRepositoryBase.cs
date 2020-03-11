using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Withywoods.Net.Http.Exceptions;
using Withywoods.Serialization.Json;

namespace Withywoods.Net.Http
{
    /// <summary>
    /// Abstract class for HTTP repositories, making HTTP calls to REST API to read & update data.
    /// </summary>
    public abstract class HttpRepositoryBase
    {
        /// <summary>
        /// Assign required services during object instanciation.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="httpClientFactory"></param>
        protected HttpRepositoryBase(ILogger logger, IHttpClientFactory httpClientFactory)
        {
            Logger = logger;
            HttpClientFactory = httpClientFactory;
        }

        /// <summary>
        /// Logger.
        /// </summary>
        protected ILogger Logger { get; private set; }

        /// <summary>
        /// HttpClient factory.
        /// </summary>
        protected IHttpClientFactory HttpClientFactory { get; private set; }

        /// <summary>
        /// HTTP client name, gives the possibility to customize the calls to an API (authentication in particular).
        /// Can be left empty.
        /// </summary>
        protected abstract string HttpClientName { get; }

        /// <summary>
        /// Make a GET call to the provided URL and cast back the result to <see cref="T"/>.
        /// The method excpects a successful response, otherwise an Exception is raised.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <returns></returns>
        /// <exception cref="ConnectivityException"></exception>
        protected virtual async Task<T> GetAsync<T>(string url) where T : class
        {
            var client = string.IsNullOrEmpty(HttpClientName) ? HttpClientFactory.CreateClient()
                : HttpClientFactory.CreateClient(HttpClientName);

            var response = await client.GetAsync(url);

            var stringResult = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                Logger.LogDebug($"Status code doesn't indicate success [HttpRequestUrl={url}] [HttpResponseStatusCode={response.StatusCode}] [HttpResponseContent={stringResult ?? ""}]");
                throw new ConnectivityException($"The response status \"{response.StatusCode}\" is not a success (reason=\"{response.ReasonPhrase}\")");
            }

            if (string.IsNullOrEmpty(stringResult))
            {
                throw new ConnectivityException($"Empty response received while calling {url}");
            }

            try
            {
                return stringResult.FromJson<T>();
            }
            catch (Exception exc)
            {
                Logger.LogWarning($"Cannot deserialize GET call response content [HttpRequestUrl={url}] [HttpResponseContent={stringResult}] [SerializationType={typeof(T).ToString()}] [ExceptionMessage={exc.Message}]");
                Logger.LogDebug($"[Stacktrace={exc.StackTrace}]");
                throw new ConnectivityException($"Invalid data received when calling \"{url}\". {exc.Message}.", exc);
            }
        }
    }
}
