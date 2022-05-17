using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Withywoods.AspNetCoreApiSample.Dto;
using Withywoods.AspNetCoreApiSample.IntegrationTests.Entities;
using Withywoods.Serialization.Json;
using Withywoods.WebTesting.Rest;
using Xunit;

namespace Withywoods.AspNetCoreApiSample.IntegrationTests.Resources
{
    [Trait("Environment", "Localhost")]
    public class TaskResourceTest : RestClient, IClassFixture<WebApplicationFactory<Program>>
    {
        private const string ResourceEndpoint = "api/tasks";

        private readonly Fixture _fixture;

        private readonly RestRunner _restRunner;

        public TaskResourceTest(WebApplicationFactory<Program> factory)
            : base(factory.CreateClient())
        {
            _fixture = new Fixture();
            _restRunner = new RestRunner(_fixture, this, ResourceEndpoint);
        }

        [Fact]
        public async Task AspNetCoreApiSampleTaskResourcePost_ReturnsOk()
        {
            var input = _fixture.Create<TaskDto>();
            var response = await PostAsync($"/{ResourceEndpoint}", input.ToJson(), HttpStatusCode.Created);

            dynamic deserializedValue = JsonConvert.DeserializeObject(response);
            await DeleteAsync($"/{ResourceEndpoint}/{deserializedValue["id"]}");
        }

        [Fact]
        public async Task AspNetCoreApiSampleTaskResourceFullCycle_IsOk()
        {
            var initialTasks = await GetAsync<List<TaskDto>>($"/{ResourceEndpoint}");
            initialTasks.Count.Should().Be(0);

            var created = await _restRunner.CreateResourceAsync<TaskDto>();

            await _restRunner.GetResourceByIdAsync(created.Id, created);

            await _restRunner.UpdateResourceAsync(created.Id, created);

            await _restRunner.PatchResourceAsync(created.Id, created);

            var existingTasks = await _restRunner.GetResourcesAsync<TaskDto>();
            existingTasks.Count.Should().Be(1);

            await _restRunner.DeleteResourceAsync(created.Id);

            var expectedNotFound = new ProblemDetails
            {
                Title = "Not Found",
                Status = 404,
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4"
            };
            await _restRunner.GetResourceByIdAsync(created.Id, expectedNotFound, HttpStatusCode.NotFound, config => config.Excluding(x => x.Extensions));

            var finalTasks = await _restRunner.GetResourcesAsync<TaskDto>();
            finalTasks.Count.Should().Be(0);
        }

        [Fact]
        public Task AspNetCoreApiSampleTaskResourceDelete_WhenResourceDoesNotExist_ReturnsHttpNoContent()
        {
            return _restRunner.DeleteResourceAsync(new Guid().ToString());
        }

        [Fact]
        public async Task AspNetCoreApiSampleTaskResourceUpdate_WhenResourceDoesNotExist_ReturnsHttpNotFound()
        {
            var taskId = Guid.NewGuid().ToString();
            var expectedNotFound = new ProblemDetails
            {
                Title = "Not Found",
                Status = 404,
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4"
            };
            await _restRunner.UpdateResourceAsync(taskId, new TaskDto { Id = taskId, Title = "Bla bla" }, expectedNotFound, HttpStatusCode.NotFound,
                config => config.Excluding(x => x.Extensions));
        }

        [Fact]
        public async Task AspNetCoreApiSampleTaskResourceUpdate_WhenResourceDoesNotExistAndNewTitleIsNull_ReturnsHttpBadRequest()
        {
            var taskId = Guid.NewGuid().ToString();
            var expectedError = new ValidationProblemDetails
            {
                Title = "One or more validation errors occurred.",
                Status = 400,
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
            };
            expectedError.Errors["Title"] = new string[1] { "The Title field is required." };
            await _restRunner.UpdateResourceAsync(taskId, new TaskDto { Id = taskId }, expectedError, HttpStatusCode.BadRequest,
                config => config.Excluding(x => x.Extensions));
        }

        [Fact]
        public async Task AspNetCoreApiSampleTaskResourceUpdate_WhenResourceDoesNotExistAndNewTitleIsNull_ReturnsHttpLightBadRequest()
        {
            var taskId = Guid.NewGuid().ToString();
            var expectedError = new LightValidationProblemDetails
            {
                Title = "One or more validation errors occurred.",
                Status = 400,
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
            };
            expectedError.Errors["Title"] = new string[1] { "The Title field is required." };
            await _restRunner.UpdateResourceAsync(taskId, new TaskDto { Id = taskId }, expectedError, HttpStatusCode.BadRequest);
        }

        [Theory]
        [InlineData("dummy", "")]
        [InlineData("dummy", "42")]
        public async Task AspNetCoreApiSampleTaskResourceUpdate_WhenInvalidIdProvided_ReturnsHttpBadRequest(string id, string resourceId)
        {
            var expectedError = new ProblemDetails
            {
                Title = "Bad Request",
                Status = 400,
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
            };
            await _restRunner.UpdateResourceAsync(id, new TaskDto { Id = resourceId, Title = "Bla bla" }, expectedError, HttpStatusCode.BadRequest,
                config => config.Excluding(x => x.Extensions));
        }

        [Fact]
        public async Task AspNetCoreApiSampleTaskResourceUpdate_WhenNullDto_ReturnsHttpBadRequest()
        {
            var expectedError = new ProblemDetails
            {
                Title = "One or more validation errors occurred.",
                Status = 400,
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
            };
            await _restRunner.UpdateResourceAsync("dummy", (TaskDto)null, expectedError, HttpStatusCode.BadRequest,
                config => config.Excluding(x => x.Extensions));
        }

        [Fact]
        public async Task AspNetCoreApiSampleTaskResourcePatch_WhenResourceDoesNotExist_ReturnsHttpNotFound()
        {
            var taskId = Guid.NewGuid().ToString();
            var expectedNotFound = new ProblemDetails
            {
                Title = "Not Found",
                Status = 404,
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4"
            };
            await _restRunner.PatchResourceAsync(taskId, new TaskPatchDto { Title = "Bla bla bla bla" }, expectedNotFound, HttpStatusCode.NotFound,
                config => config.Excluding(x => x.Extensions));
        }

        [Fact]
        public async Task AspNetCoreApiSampleTaskResourcePatch_WhenResourceDoesNotExistAndNewTitleIsNull_ReturnsHttpNotFound()
        {
            var taskId = Guid.NewGuid().ToString();
            var expectedError = new ValidationProblemDetails
            {
                Title = "Not Found",
                Status = 404,
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4"
            };
            await _restRunner.PatchResourceAsync(taskId, new TaskPatchDto { Id = taskId }, expectedError, HttpStatusCode.NotFound,
                config => config.Excluding(x => x.Extensions));
        }

        [Fact]
        public async Task AspNetCoreApiSampleTaskResourcePatch_WhenNullDto_ReturnsHttpBadRequest()
        {
            var expectedError = new ProblemDetails
            {
                Title = "One or more validation errors occurred.",
                Status = 400,
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
            };
            await _restRunner.PatchResourceAsync("dummy", (TaskPatchDto)null, expectedError, HttpStatusCode.BadRequest,
                config => config.Excluding(x => x.Extensions));
        }

        [Fact]
        public async Task AspNetCoreApiSampleTaskResourcePatch_WhenModifyOneProperty_ReturnsExpectedObject()
        {
            // Arrange
            var created = await _restRunner.CreateResourceAsync<TaskPatchDto>();
            dynamic patch1 = new { IsComplete = true };
            dynamic patch2 = new { Title = string.Empty };
            var expected1 = new TaskPatchDto { Id = created.Id, Title = created.Title, IsComplete = true };
            var expected2 = new TaskPatchDto { Id = created.Id, Title = string.Empty, IsComplete = true };

            // Act & Assert
            await _restRunner.PatchResourceAsync(created.Id, patch1);
            await _restRunner.GetResourceByIdAsync(created.Id, expected1);
            await _restRunner.PatchResourceAsync(created.Id, patch2);
            await _restRunner.GetResourceByIdAsync(created.Id, expected2);

            await DeleteAsync($"/{ResourceEndpoint}/{created.Id}");
        }
    }
}
