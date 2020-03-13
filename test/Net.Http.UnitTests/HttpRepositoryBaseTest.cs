using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Withywoods.Net.Http.Exceptions;
using Withywoods.Net.Http.UnitTests.Fakes;
using Withywoods.Serialization.Json;
using Withywoods.UnitTesting;
using Xunit;

namespace Withywoods.Net.Http.UnitTests
{
    [Trait("Category", "UnitTests")]
    public class HttpRepositoryBaseTest : HttpRepositoryTestBase
    {
        [Fact]
        public async Task FakeHttpRepositoryFindAllAsync_ReturnSuccess()
        {
            // Arrange
            var fixture = new Fixture();
            var responseDto = fixture.Create<List<string>>();
            var httpResponseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(responseDto.ToJson())
            };
            var repository = BuildRepository(httpResponseMessage, HttpMethod.Get, "https://does.not.exist/v42/api/fakes");

            // Act
            var output = await repository.FindAllAsync();

            // Assert
            output.Should().NotBeNullOrEmpty();
            output.Count.Should().Be(responseDto.Count);
        }

        [Fact]
        public async Task FakeHttpRepositoryFindAllAsync_WhenErrorStatus_ThrowsException()
        {
            // Arrange
            var httpResponseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.BadRequest,
                ReasonPhrase = "No particular reason",
                Content = new StringContent("Dude where is my car?")
            };
            var repository = BuildRepository(httpResponseMessage, HttpMethod.Get, "https://does.not.exist/v42/api/fakes");

            // Act
            var exc = await Assert.ThrowsAsync<ConnectivityException>(async () => await repository.FindAllAsync());

            // Assert
            exc.Should().NotBeNull();
            exc.Message.Should().Be("The response status \"BadRequest\" is not a success (reason=\"No particular reason\")");
        }

        [Fact]
        public async Task FakeHttpRepositoryFindAllAsync_WhenEmptyResult_ThrowsException()
        {
            // Arrange
            var httpResponseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(string.Empty)
            };
            var repository = BuildRepository(httpResponseMessage, HttpMethod.Get, "https://does.not.exist/v42/api/fakes");

            // Act
            var exc = await Assert.ThrowsAsync<ConnectivityException>(async () => await repository.FindAllAsync());

            // Assert
            exc.Should().NotBeNull();
            exc.Message.Should().Be("Empty response received while calling https://does.not.exist/v42/api/fakes");
        }

        [Fact]
        public async Task FakeHttpRepositoryFindAllAsync_WhenInvalidData_ThrowsException()
        {
            // Arrange
            var fixture = new Fixture();
            var responseDto = fixture.Create<int>();
            var httpResponseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(responseDto.ToJson())
            };
            var repository = BuildRepository(httpResponseMessage, HttpMethod.Get, "https://does.not.exist/v42/api/fakes");

            // Act
            var exc = await Assert.ThrowsAsync<ConnectivityException>(async () => await repository.FindAllAsync());

            // Assert
            exc.Should().NotBeNull();
            exc.Message.Should().Contain("Invalid data received when calling \"https://does.not.exist/v42/api/fakes\". Error converting");
        }

        [Fact]
        public async Task FakeHttpRepositoryFindAllAsync_WhenInvalidMock_ThrowsException()
        {
            // Arrange
            var fixture = new Fixture();
            var responseDto = fixture.Create<int>();
            var httpResponseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(responseDto.ToJson())
            };
            var repository = BuildRepository(httpResponseMessage, HttpMethod.Get, "https://does.not.exist/v13/api/fakes");

            // Act
            var exc = await Assert.ThrowsAsync<NotImplementedException>(async () => await repository.FindAllAsync());

            // Assert
            exc.Should().NotBeNull();
            exc.Message.Should().Contain("This code shouldn't be executed, the call to https://does.not.exist/v42/api/fakes must be mocked.");
        }

        [Fact]
        public async Task FakeHttpRepositoryFindAllAsync_WhenUnnamedClient_ReturnSuccess()
        {
            // Arrange
            var fixture = new Fixture();
            var responseDto = fixture.Create<List<string>>();
            var httpResponseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(responseDto.ToJson())
            };
            var repository = BuildUnnamedClientRepository(httpResponseMessage, HttpMethod.Get, "https://still.not.here/api/fakes");

            // Act
            var output = await repository.FindAllAsync();

            // Assert
            output.Should().NotBeNullOrEmpty();
            output.Count.Should().Be(responseDto.Count);
        }

        private FakeHttpRepository BuildRepository(HttpResponseMessage httpResponseMessage, HttpMethod httpMethod, string absoluteUri)
        {
            var logger = ServiceProvider.GetService<ILogger<FakeHttpRepository>>();
            var httpClientFactoryMock = BuildHttpClientFactory(httpResponseMessage, httpMethod, "FakeApi", absoluteUri);

            return new FakeHttpRepository(logger, httpClientFactoryMock.Object);
        }

        private FakeHttpUnnamedClientRepository BuildUnnamedClientRepository(HttpResponseMessage httpResponseMessage, HttpMethod httpMethod, string absoluteUri)
        {
            return new FakeHttpUnnamedClientRepository(
                ServiceProvider.GetService<ILogger<FakeHttpRepository>>(),
                BuildHttpClientFactory(httpResponseMessage, httpMethod, "", absoluteUri).Object);
        }
    }
}
