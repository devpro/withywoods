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

        private readonly ResourceTestBase _resource;

        private readonly string _resourceEndpoint;

        /// <summary>
        /// Creates a new instance of <see cref="RestRunner"/>.
        /// </summary>
        /// <param name="fixture"></param>
        /// <param name="resource"></param>
        public RestRunner(Fixture fixture, ResourceTestBase resource, string resourceEndpoint)
        {
            _fixture = fixture;
            _resource = resource;
            _resourceEndpoint = resourceEndpoint;
        }

        /// <summary>
        /// Get a resource by it's id.
        /// </summary>
        /// <typeparam name="T">Type of the resource</typeparam>
        /// <param name="id">Resource id</param>
        /// <param name="expected">Expected output from the request</param>
        /// <param name="httpStatusCode">Expected HTTP status code, OK by default (HTTP 200)</param>
        /// <param name="config">Comparison configuration</param>
        /// <returns></returns>
        public async Task<T> GetResourceByIdAsync<T>(
            string id,
            T expected,
            HttpStatusCode httpStatusCode = HttpStatusCode.OK,
            Func<EquivalencyAssertionOptions<T>, EquivalencyAssertionOptions<T>> config = null)
        {
            var output = await _resource.GetAsync<T>($"/{_resourceEndpoint}/{id}", httpStatusCode);
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
        /// <returns></returns>
        public async Task<List<T>> GetResourcesAsync<T>()
        {
            return await _resource.GetAsync<List<T>>($"/{_resourceEndpoint}");
        }

        /// <summary>
        /// Creates a resource.
        /// </summary>
        /// <typeparam name="T">Type of the resource</typeparam>
        /// <returns></returns>
        public async Task<T> CreateResourceAsync<T>()
        {
            var input = _fixture.Create<T>();
            var idField = typeof(T).GetProperty("Id");
            if (idField != null)
            {
                idField.SetValue(input, null);
            }
            var created = await _resource.PostAsync<T>($"/{_resourceEndpoint}", new StringContent(input.ToJson(), Encoding.UTF8, "application/json"));
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
        /// <returns></returns>
        public Task UpdateResourceAsync(string id, object input)
        {
            return UpdateResourceAsync<object>(id, input);
        }

        /// <summary>
        /// Updates a resource of type <see cref="T"/> by making an UPDATE HTTP request.
        /// </summary>
        /// <typeparam name="T">Type of the resource</typeparam>
        /// <param name="id">Resource id</param>
        /// <param name="input">New values for the resource</param>
        /// <param name="httpStatusCode">Expected HTTP status code, OK by default (HTTP 200)</param>
        /// <param name="config">Comparison configuration</param>
        /// <returns></returns>
        public async Task UpdateResourceAsync<T>(
            string id,
            object input,
            T expected = null,
            HttpStatusCode httpStatusCode = HttpStatusCode.NoContent,
            Func<EquivalencyAssertionOptions<T>, EquivalencyAssertionOptions<T>> config = null)
            where T : class
        {
            var output = await _resource.PutAsync<T>($"/{_resourceEndpoint}/{id}", input.ToJson(), httpStatusCode);
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
        /// <param name="httpStatusCode">Expected HTTP status code returned</param>
        /// <returns></returns>
        public async Task DeleteResourceAsync(string id, HttpStatusCode httpStatusCode = HttpStatusCode.NoContent)
        {
            var stringResponse = await _resource.DeleteAsync($"/{_resourceEndpoint}/{id}", httpStatusCode);
            stringResponse.Should().Be(string.Empty);
        }
    }
}
