using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using FluentAssertions.Equivalency;
using Withywoods.Serialization.Json;

namespace Withywoods.WebTesting.Rest
{
    /// <summary>
    /// REST runner to ease web API testing.
    /// </summary>
    public class RestRunner
    {
        private readonly Fixture _fixture;

        /// <summary>
        /// Creates a new instance of <see cref="RestRunner"/>.
        /// </summary>
        public RestRunner()
        {
            _fixture = new Fixture();
        }

        /// <summary>
        /// Resource endpoint.
        /// </summary>
        public string ResourceEndpoint { get; set; }

        /// <summary>
        /// Get a resource by it's id.
        /// </summary>
        /// <typeparam name="T">Type of the resource</typeparam>
        /// <param name="id">Resource id</param>
        /// <param name="httpClient">HTTP client to be used</param>
        /// <param name="expected">Expected output from the request</param>
        /// <param name="httpStatusCode">Expected HTTP status code, OK by default (HTTP 200)</param>
        /// <param name="config">Comparison configuration</param>
        /// <returns></returns>
        public async Task<T> GetResourceById<T>(string id, HttpClient httpClient, T expected,
            HttpStatusCode httpStatusCode = HttpStatusCode.OK, Func<EquivalencyAssertionOptions<T>, EquivalencyAssertionOptions<T>> config = null)
        {
            var response = await httpClient.GetAsync($"/{ResourceEndpoint}/{id}");
            response.StatusCode.Should().Be(httpStatusCode);
            var stringResponse = await response.Content.ReadAsStringAsync();
            var output = stringResponse.FromJson<T>();
            output.Should().NotBeNull();
            if (config != null)
            {
                output.Should().BeEquivalentTo(expected, config);
            }
            else
            {
                output.Should().BeEquivalentTo(expected);
            }
            return output;
        }

        /// <summary>
        /// Gets all resources.
        /// </summary>
        /// <typeparam name="T">Type of the resource</typeparam>
        /// <param name="httpClient">HTTP client to be used</param>
        /// <returns></returns>
        public async Task<List<T>> GetResources<T>(HttpClient httpClient)
        {
            var response = await httpClient.GetAsync($"/{ResourceEndpoint}");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var stringResponse = await response.Content.ReadAsStringAsync();
            var output = stringResponse.FromJson<List<T>>();
            output.Should().NotBeNull();
            return output;
        }

        /// <summary>
        /// Creates a resource.
        /// </summary>
        /// <typeparam name="T">Type of the resource</typeparam>
        /// <param name="httpClient">HTTP client to be used</param>
        /// <returns></returns>
        public async Task<T> CreateResource<T>(HttpClient httpClient)
        {
            var input = _fixture.Create<T>();
            var idField = typeof(T).GetProperty("Id");
            if (idField != null)
            {
                idField.SetValue(input, null);
            }
            var response = await httpClient.PostAsync($"/{ResourceEndpoint}", new StringContent(input.ToJson(), Encoding.UTF8, "application/json"));
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            var stringResponse = await response.Content.ReadAsStringAsync();
            var created = stringResponse.FromJson<T>();
            created.Should().NotBeNull();
            if (idField != null)
            {
                var idValue = idField.GetValue(created);
                idValue.Should().NotBeNull();
                idField.SetValue(input, idValue);
            }
            created.Should().BeEquivalentTo(input);
            return input;
        }

        /// <summary>
        /// Updates a resource of type <see cref="T"/> by making an UPDATE HTTP request.
        /// </summary>
        /// <typeparam name="T">Type of the resource</typeparam>
        /// <param name="id">Resource id</param>
        /// <param name="input">New values for the resource</param>
        /// <param name="httpClient">HTTP client to be used</param>
        /// <returns></returns>
        public async Task UpdateResource<T>(string id, T input, HttpClient httpClient)
        {
            await UpdateResource<T, object>(id, input, httpClient);
        }

        /// <summary>
        /// Updates a resource of type <see cref="T"/> by making an UPDATE HTTP request.
        /// </summary>
        /// <typeparam name="T">Type of the resource</typeparam>
        /// <param name="id">Resource id</param>
        /// <param name="input">New values for the resource</param>
        /// <param name="httpClient">HTTP client to be used</param>
        /// <param name="httpStatusCode">Expected HTTP status code, OK by default (HTTP 200)</param>
        /// <param name="config">Comparison configuration</param>
        /// <returns></returns>
        public async Task UpdateResource<T, U>(string id, T input, HttpClient httpClient, U expected = null,
            HttpStatusCode httpStatusCode = HttpStatusCode.NoContent, Func<EquivalencyAssertionOptions<U>, EquivalencyAssertionOptions<U>> config = null)
            where U : class
        {
            var response = await httpClient.PutAsync($"/{ResourceEndpoint}/{id}", new StringContent(input.ToJson(), Encoding.UTF8, "application/json"));
            response.StatusCode.Should().Be(httpStatusCode);
            var stringResponse = await response.Content.ReadAsStringAsync();
            if (expected == null)
            {
                stringResponse.Should().Be(string.Empty);
                return;
            }

            var output = stringResponse.FromJson<U>();
            output.Should().NotBeNull();
            if (config != null)
            {
                output.Should().BeEquivalentTo(expected, config);
            }
            else
            {
                output.Should().BeEquivalentTo(expected);
            }
        }

        /// <summary>
        /// Deletes a resource by making a DELETE HTTP request.
        /// </summary>
        /// <param name="id">Resource id</param>
        /// <param name="httpClient">HTTP client to be used</param>
        /// <returns></returns>
        public async Task DeleteResource(string id, HttpClient httpClient, HttpStatusCode httpStatusCode = HttpStatusCode.NoContent)
        {
            var response = await httpClient.DeleteAsync($"/{ResourceEndpoint}/{id}");
            response.StatusCode.Should().Be(httpStatusCode);
            var stringResponse = await response.Content.ReadAsStringAsync();
            stringResponse.Should().Be(string.Empty);
        }
    }
}
