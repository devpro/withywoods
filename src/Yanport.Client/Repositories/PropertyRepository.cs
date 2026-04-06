using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Withywoods.Net.Http;
using Withywoods.Yanport.Abstractions.Models;
using Withywoods.Yanport.Abstractions.Repositories;

namespace Withywoods.Yanport.Client.Repositories;

public class PropertyRepository(
    IYanportClientConfiguration configuration,
    ILogger<PropertyRepository> logger,
    IHttpClientFactory httpClientFactory)
    : RepositoryBase(configuration, logger, httpClientFactory), IPropertyRepository
{
    protected override string ResourceName => "properties";

    public async Task<List<HitModel>> FindAllAsync(string queryParameters = "")
    {
        var url = GenerateUrl(arguments: queryParameters);
        var output = await GetAsync<ResultModel>(url);
        return output?.Hits ?? [];
    }

    protected override async Task<T?> GetAsync<T>(string url) where T : class
    {
        var client = string.IsNullOrEmpty(HttpClientName) ? HttpClientFactory.CreateClient()
            : HttpClientFactory.CreateClient(HttpClientName);

        var response = await client.GetAsync(url);

        var stringResult = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            if (Logger.IsEnabled(LogLevel.Debug))
            {
                Logger.LogDebug(
                    "Status code doesn't indicate success [HttpRequestUrl={Url}] [HttpResponseStatusCode={ResponseStatusCode}] [HttpResponseContent={StringResult}]",
                    url, response.StatusCode, stringResult);
            }

            throw new ConnectivityException($"The response status \"{response.StatusCode}\" is not a success (reason=\"{response.ReasonPhrase}\")");
        }

        try
        {
            return JsonSerializer.Deserialize<T>(stringResult);
        }
        catch (Exception exc)
        {
            Logger.LogWarning("Cannot deserialize GET call response content [HttpRequestUrl={Url}] [SerializationType={Type}] [ExceptionMessage={ExcMessage}]",
                url, typeof(T), exc.Message);
            if (Logger.IsEnabled(LogLevel.Debug))
            {
                Logger.LogDebug("[HttpResponseContent={StringResult}]", stringResult);
                Logger.LogDebug("[Stacktrace={ExcStackTrace}]", exc.StackTrace);
            }

            throw new ConnectivityException($"Invalid data received when calling \"{url}\". {exc.Message}.", exc);
        }
    }
}
