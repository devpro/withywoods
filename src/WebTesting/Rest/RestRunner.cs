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
    public class RestRunner
    {
        private readonly Fixture _fixture;

        public RestRunner()
        {
            _fixture = new Fixture();
        }

        public string RessourceEndpoint { get; set; }

        public async Task<T> GetResourceById<T>(string id, HttpClient httpClient, T expected,
            HttpStatusCode httpStatusCode = HttpStatusCode.OK, Func<EquivalencyAssertionOptions<T>, EquivalencyAssertionOptions<T>> config = null)
        {
            var response = await httpClient.GetAsync($"/{RessourceEndpoint}/{id}");
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

        public async Task<List<T>> GetResources<T>(HttpClient httpClient)
        {
            var response = await httpClient.GetAsync($"/{RessourceEndpoint}");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var stringResponse = await response.Content.ReadAsStringAsync();
            var output = stringResponse.FromJson<List<T>>();
            output.Should().NotBeNull();
            return output;
        }

        public async Task<T> CreateResource<T>(HttpClient httpClient)
        {
            var input = _fixture.Create<T>();
            var idField = typeof(T).GetProperty("Id");
            if (idField != null)
            {
                idField.SetValue(input, null);
            }
            var response = await httpClient.PostAsync($"/{RessourceEndpoint}", new StringContent(input.ToJson(), Encoding.UTF8, "application/json"));
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

        public async Task UpdateResource<T>(string id, T input, HttpClient httpClient)
        {
            var response = await httpClient.PutAsync($"/{RessourceEndpoint}/{id}", new StringContent(input.ToJson(), Encoding.UTF8, "application/json"));
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
            var stringResponse = await response.Content.ReadAsStringAsync();
            stringResponse.Should().Be(string.Empty);
        }

        public async Task DeleteResource<T>(string id, HttpClient httpClient)
        {
            var response = await httpClient.DeleteAsync($"/{RessourceEndpoint}/{id}");
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
            var stringResponse = await response.Content.ReadAsStringAsync();
            stringResponse.Should().Be(string.Empty);
        }
    }
}
