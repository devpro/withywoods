# Withywoods Web Testing

This library can be used by an ASP.NET Core 3.1 applications.

## Features

- Provide two classes to ease web testing:

  - `LocalServerFactory`: to be able to use Selenium on a ASP.NET test server (integration tests)

  ```csharp
  public class SwaggerResourceTest : IClassFixture<LocalServerFactory<Startup>>, IDisposable
  {
      private const string _ResourceEndpoint = "swagger";

      private readonly HttpClient _httpClient;
      private readonly RemoteWebDriver _webDriver;
      private readonly LocalServerFactory<Startup> _server;

      public SwaggerResourceTest(LocalServerFactory<Startup> server)
      {
          _server = server;
          _httpClient = server.CreateClient();

          var chromeOptions = new ChromeOptions();
          chromeOptions.AddArguments("--headless");
          var executingAssembly = System.Reflection.Assembly.GetExecutingAssembly();
          var currentLocation = System.IO.Path.GetDirectoryName(executingAssembly.Location);
          _webDriver = new ChromeDriver(currentLocation, chromeOptions);
      }

      [Fact]
      public void AspNetCoreApiSampleSwaggerResourceGet_ReturnsHttpOk()
          {
          // Arrange & Act
          _webDriver.Navigate().GoToUrl($"{_server.RootUri}/{_ResourceEndpoint}");

          // Assert
          _webDriver.FindElement(By.ClassName("title"), 360);
          _webDriver.Title.Should().Be("Swagger UI");
          _webDriver.FindElementByClassName("title").Text.Should().Contain("My API");
      }
  }
  ```

  - `RestRunner`: to ease API testing

  ```csharp
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
  }
  ```

- Examples are available in the ASP.NET Core application sample testing project (located in `test/AspNetCoreApiSample.IntegrationTests`)
