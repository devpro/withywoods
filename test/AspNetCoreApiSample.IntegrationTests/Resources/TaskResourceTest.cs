﻿using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Withywoods.AspNetCoreApiSample.Dto;
using Withywoods.AspNetCoreApiSample.IntegrationTests.Entities;
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
            _restRunner = new RestRunner { ResourceEndpoint = _ResourceEndpoint };
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

            await _restRunner.DeleteResource(created.Id, _client);

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

        [Fact]
        public async Task AspNetCoreApiSampleTaskResourceDelete_WhenResourceDoesNotExist_ReturnsHttpNoContent()
        {
            await _restRunner.DeleteResource(new Guid().ToString(), _client);
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
            await _restRunner.UpdateResource(taskId, new TaskDto { Id = taskId, Title = "Bla bla" }, _client, expectedNotFound, HttpStatusCode.NotFound,
                config => config.Excluding(x => x.Extensions));
        }

        [Fact]
        public async Task AspNetCoreApiSampleTaskResourceUpdate_WhenResourceDoesNotExistAndNewTitleIsNull_ReturnsHttpBadRequest()
        {
            var taskId = Guid.NewGuid().ToString();
            var expectedError = new ValidationProblemDetails
            {
                Title = "One or more validation errors occurred.",
                Status = 400
            };
            expectedError.Errors["Title"] = new string[1] { "The Title field is required." };
            await _restRunner.UpdateResource(taskId, new TaskDto { Id = taskId }, _client, expectedError, HttpStatusCode.BadRequest,
                config => config.Excluding(x => x.Extensions));
        }

        [Fact]
        public async Task AspNetCoreApiSampleTaskResourceUpdate_WhenResourceDoesNotExistAndNewTitleIsNull_ReturnsHttpLighBadRequest()
        {
            var taskId = Guid.NewGuid().ToString();
            var expectedError = new LightValidationProblemDetails
            {
                Title = "One or more validation errors occurred.",
                Status = 400
            };
            expectedError.Errors["Title"] = new string[1] { "The Title field is required." };
            await _restRunner.UpdateResource(taskId, new TaskDto { Id = taskId }, _client, expectedError, HttpStatusCode.BadRequest);
        }

        [Theory]
        [InlineData("dummy", null)]
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
            await _restRunner.UpdateResource(id, new TaskDto { Id = resourceId, Title = "Bla bla" }, _client, expectedError, HttpStatusCode.BadRequest,
                config => config.Excluding(x => x.Extensions));
        }
    }
}
