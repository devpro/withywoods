using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Withywoods.AspNetCoreApiSample.Dto;
using Withywoods.WebTesting.Rest;
using Xunit;

namespace Withywoods.AspNetCoreApiSample.IntegrationTests.Resources
{
    [Trait("Environment", "Localhost")]
    public class TaskResourceTest : IClassFixture<WebApplicationFactory<Startup>>
    {
        private const string _ResourceEndpoint = "api/tasks";

        private readonly HttpClient _client;
        private readonly RestRunner _restRunner;

        public TaskResourceTest(WebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
            _restRunner = new RestRunner { RessourceEndpoint = _ResourceEndpoint };
        }

        [Fact]
        public async Task AspNetCoreApiSampleTaskResourceFullCycle_IsOk()
        {
            var initialTasks = await _restRunner.GetResources<TaskDto>(_client);
            initialTasks.Count.Should().Be(0);

            var created = await _restRunner.CreateResource<TaskDto>(_client);

            await _restRunner.GetResourceById(created.Id, _client, created);

            await _restRunner.UpdateResource(created.Id, created, _client);

            var existingTasks = await _restRunner.GetResources<TaskDto>(_client);
            existingTasks.Count.Should().Be(1);

            await _restRunner.DeleteResource<TaskDto>(created.Id, _client);

            var expectedNotFound = new ProblemDetails
            {
                Title = "Not Found",
                Status = 404,
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4"
            };
            await _restRunner.GetResourceById(created.Id, _client, expectedNotFound, HttpStatusCode.NotFound, config => config.Excluding(x => x.Extensions));

            var finalTasks = await _restRunner.GetResources<TaskDto>(_client);
            finalTasks.Count.Should().Be(0);
        }
    }
}
