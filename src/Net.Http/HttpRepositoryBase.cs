using System;
using System.Net;
using System.Net.Http;
using System.Text;
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
        #region Constructor & protected fields

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

        #endregion

        #region Protected methods

        /// <summary>
        /// Enrich the HTTP client object that will be used in the HTTP calls.
        /// Can be interesting for example to set a token that is generated on the fly.
        /// For enrichment that comes from application configuration, it's better to do it while creating the HttpClients in the Startup class.
        /// </summary>
        /// <param name="client"></param>
        protected virtual void EnrichRequestHeaders(HttpClient client)
        {
        }

        /// <summary>
        /// Make a GET call to the provided URL and cast back the result to <see cref="T"/>.
        /// The method expects a successful response, otherwise an Exception is raised.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <returns></returns>
        /// <exception cref="ConnectivityException"></exception>
        protected virtual async Task<T> GetAsync<T>(string url) where T : class
        {
            var client = CreateHttpClient();

            var response = await client.GetAsync(url);

            var stringResult = await response.Content.ReadAsStringAsync();

            CheckStatusCodeAndResult(url, response, stringResult);

            return Deserialize<T>(url, "GET", stringResult);
        }

        /// <summary>
        /// Make a POST call to the provided URL and cast back the result to <see cref="T"/>.
        /// The method expects a successful response, otherwise an Exception is raised.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="body">Message body content (object)</param>
        /// <param name="mediaType">Message body media type, "application/json" by default</param>
        /// <returns></returns>
        /// <exception cref="ConnectivityException"></exception>
        protected virtual async Task<T> PostAsync<T>(string url, object body, string mediaType = "application/json") where T : class
        {
            var client = CreateHttpClient();

            var response = await client.PostAsync(url, new StringContent(body.ToJson(), Encoding.UTF8, mediaType));

            var stringResult = await response.Content.ReadAsStringAsync();

            CheckStatusCodeAndResult(url, response, stringResult);

            return Deserialize<T>(url, "POST", stringResult);
        }

        /// <summary>
        /// Make a PUT call to the provided URL.
        /// The method expects a successful response, otherwise an Exception is raised.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="body">Message body content (object)</param>
        /// <param name="mediaType">Message body media type, "application/json" by default</param>
        /// <returns></returns>
        /// <exception cref="ConnectivityException"></exception>
        protected virtual async Task PutAsync(string url, object body, string mediaType = "application/json")
        {
            var client = CreateHttpClient();

            var response = await client.PutAsync(url, new StringContent(body.ToJson(), Encoding.UTF8, mediaType));

            var stringResult = response.Content != null ? await response.Content.ReadAsStringAsync() : string.Empty;

            CheckStatusCode(url, response, stringResult);
        }

        /// <summary>
        /// Make a DELETE call to the provided URL.
        /// The method expects a successful response, otherwise an Exception is raised.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="ignoreNotFound">Should we ignore not found errors? (404) The resource may have been deleted already</param>
        /// <returns></returns>
        /// <exception cref="ConnectivityException"></exception>
        protected virtual async Task DeleteAsync(string url, bool ignoreNotFound)
        {
            var client = CreateHttpClient();

            var response = await client.DeleteAsync(url);

            var stringResult = response.Content != null ? await response.Content.ReadAsStringAsync() : string.Empty;

            if (!ignoreNotFound || response.StatusCode != HttpStatusCode.NotFound)
            {
                CheckStatusCode(url, response, stringResult);
            }
        }

        #endregion

        #region Private methods

        private HttpClient CreateHttpClient()
        {
            var client = string.IsNullOrEmpty(HttpClientName) ? HttpClientFactory.CreateClient()
                : HttpClientFactory.CreateClient(HttpClientName);
            EnrichRequestHeaders(client);
            return client;
        }

        private void CheckStatusCode(string url, HttpResponseMessage response, string result)
        {
            if (!response.IsSuccessStatusCode)
            {
                Logger.LogDebug($"Status code doesn't indicate success [HttpRequestUrl={url}] [HttpResponseStatusCode={response.StatusCode}] [HttpResponseContent={result}]");
                throw new ConnectivityException($"The response status \"{response.StatusCode}\" is not a success (reason=\"{response.ReasonPhrase}\")");
            }
        }

        private void CheckStatusCodeAndResult(string url, HttpResponseMessage response, string result)
        {
            CheckStatusCode(url, response, result);

            if (string.IsNullOrEmpty(result))
            {
                throw new ConnectivityException($"Empty response received while calling {url}");
            }
        }

        private T Deserialize<T>(string url, string httpMethodName, string result)
        {
            try
            {
                return result.FromJson<T>();
            }
            catch (Exception exc)
            {
                Logger.LogWarning($"Cannot deserialize {httpMethodName} call response content [HttpRequestUrl={url}] [SerializationType={typeof(T)}] [ExceptionMessage={exc.Message}]");
                Logger.LogDebug($"[HttpResponseContent={result}]");
                Logger.LogDebug($"[Stacktrace={exc.StackTrace}]");
                throw new ConnectivityException($"Invalid data received when calling \"{url}\". {exc.Message}.", exc);
            }
        }

        #endregion
    }
}
