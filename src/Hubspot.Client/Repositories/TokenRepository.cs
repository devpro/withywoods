using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Withywoods.Hubspot.Abstractions.Models;
using Withywoods.Hubspot.Abstractions.Repositories;
using Withywoods.Net.Http;

namespace Withywoods.Hubspot.Client.Repositories;

/// <summary>
/// Token repository.
/// </summary>
/// <remarks>https://developers.hubspot.com/docs/methods/oauth2/oauth2-quickstart</remarks>
public class TokenRepository(
    IHubspotClientConfiguration configuration,
    ILogger<TokenRepository> logger,
    IHttpClientFactory httpClientFactory)
    : RepositoryBase(configuration, logger, httpClientFactory, null), ITokenRepository
{
    protected override string ResourceName => "oauth/v1/token";

    /// <summary>
    /// Create a new Token.
    /// </summary>
    /// <param name="authorizationCode"></param>
    /// <returns></returns>
    /// <remarks>https://developers.hubspot.com/docs/methods/oauth2/get-access-and-refresh-tokens</remarks>
    public async Task<TokenModel> CreateAsync(string authorizationCode)
    {
        var url = GenerateUrl();

        // TODO: make PostAsync works in this special case

        var client = HttpClientFactory.CreateClient(HttpClientName);
        client.DefaultRequestHeaders.Clear();

        var body = $"grant_type=authorization_code&client_id={Configuration.ClientId}&client_secret={Configuration.ClientSecret}&redirect_uri={Configuration.RedirectUrl}&code={authorizationCode}";
        var response = await client.PostAsync(url, new StringContent(body, Encoding.UTF8, "application/x-www-form-urlencoded"));

        var stringResult = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            Logger.LogWarning(
                "Status code doesn't indicate success [HttpRequestUrl={Url}] [HttpResponseStatusCode={ResponseStatusCode}] [HttpResponseContent={StringResult}]",
                url, response.StatusCode, stringResult);
            throw new ConnectivityException($"The response status \"{response.StatusCode}\" is not a success (reason=\"{response.ReasonPhrase}\")");
        }

        if (string.IsNullOrEmpty(stringResult))
        {
            throw new ConnectivityException($"Empty response received while calling {url}");
        }

        var model = JsonSerializer.Deserialize<TokenModel>(stringResult)
                    ?? throw new ConnectivityException($"Cannot deserialize response in TokenModel while calling {url}");
        model.CreatedAt = DateTime.UtcNow;
        model.ExpiredAt = model.CreatedAt.AddSeconds(model.ExpiresIn);
        return model;
    }
}
