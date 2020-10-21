using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Withywoods.Serialization.Json;

namespace Withywoods.WebTesting.Rest
{
    /// <summary>
    /// REST clients to ease web testing of HTTP REST resources.
    /// </summary>
    public class RestClient
    {
        private const string MediaTypeJson = "application/json";

        public RestClient(string baseAddress)
            : this(new HttpClient { BaseAddress = new Uri(baseAddress) })
        {
        }

        public RestClient(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }

        protected HttpClient HttpClient { get; }

        /// <summary>
        /// Get a resource or a list of resources.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="httpStatusCode"></param>
        /// <returns></returns>
        public async Task<string> GetAsync(string url, HttpStatusCode httpStatusCode = HttpStatusCode.OK)
        {
            var response = await HttpClient.GetAsync(url);
            response.StatusCode.Should().Be(httpStatusCode);

            return await response.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// Get a resource or a list of resources and cast it into <see cref="T"/>.
        /// </summary>
        /// <typeparam name="T">Type of the data that will be sent back</typeparam>
        /// <param name="url">Resource URL</param>
        /// <param name="httpStatusCode">Expected HTTP status code, OK by default (HTTP 200)</param>
        /// <returns></returns>
        public async Task<T> GetAsync<T>(string url, HttpStatusCode httpStatusCode = HttpStatusCode.OK)
        {
            var stringResponse = await GetAsync(url, httpStatusCode);
            var output = stringResponse.FromJson<T>();
            output.Should().NotBeNull();
            return output;
        }

        public Task<T> PatchAsync<T>(string url, string body, HttpStatusCode httpStatusCode = HttpStatusCode.NoContent)
        {
            return PatchAsync<T>(url, new StringContent(body, Encoding.UTF8, MediaTypeJson), httpStatusCode);
        }

        public async Task<T> PatchAsync<T>(string url, HttpContent bodyContent, HttpStatusCode httpStatusCode = HttpStatusCode.NoContent)
        {
            var response = await HttpClient.PatchAsync(url, bodyContent);
            response.StatusCode.Should().Be(httpStatusCode);
            var stringResponse = await response.Content.ReadAsStringAsync();

            return stringResponse.FromJson<T>();
        }

        public Task<string> PostAsync(string url, string body, HttpStatusCode httpStatusCode = HttpStatusCode.Created)
        {
            return PostAsync(url, new StringContent(body, Encoding.UTF8, MediaTypeJson), httpStatusCode);
        }

        public async Task<string> PostAsync(string url, HttpContent bodyContent, HttpStatusCode httpStatusCode = HttpStatusCode.Created)
        {
            var response = await HttpClient.PostAsync(url, bodyContent);
            response.StatusCode.Should().Be(httpStatusCode);

            return await response.Content.ReadAsStringAsync();
        }

        public Task<T> PostAsync<T>(string url, string body, HttpStatusCode httpStatusCode = HttpStatusCode.Created)
        {
            return PostAsync<T>(url, new StringContent(body, Encoding.UTF8, MediaTypeJson), httpStatusCode);
        }

        public async Task<T> PostAsync<T>(string url, HttpContent bodyContent, HttpStatusCode httpStatusCode = HttpStatusCode.Created)
        {
            var stringResponse = await PostAsync(url, bodyContent, httpStatusCode);

            var output = stringResponse.FromJson<T>();
            output.Should().NotBeNull();
            return output;
        }

        public Task<T> PutAsync<T>(string url, string input, HttpStatusCode httpStatusCode = HttpStatusCode.NoContent)
        {
            return PutAsync<T>(url, new StringContent(input, Encoding.UTF8, MediaTypeJson), httpStatusCode);
        }

        public async Task<T> PutAsync<T>(string url, HttpContent bodyContent, HttpStatusCode httpStatusCode = HttpStatusCode.NoContent)
        {
            var response = await HttpClient.PutAsync(url, bodyContent);
            response.StatusCode.Should().Be(httpStatusCode);
            var stringResponse = await response.Content.ReadAsStringAsync();

            return stringResponse.FromJson<T>();
        }

        public async Task<string> DeleteAsync(string url, HttpStatusCode httpStatusCode = HttpStatusCode.NoContent)
        {
            var response = await HttpClient.DeleteAsync(url);
            response.StatusCode.Should().Be(httpStatusCode);
            return await response.Content.ReadAsStringAsync();
        }
    }
}
