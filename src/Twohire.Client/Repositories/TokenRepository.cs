using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Withywoods.Twohire.Abstractions.Models;
using Withywoods.Twohire.Abstractions.Repositories;
using Withywoods.Twohire.Client.Dto;

namespace Withywoods.Twohire.Client.Repositories;

public class TokenRepository(
    ITwohireClientConfiguration configuration,
    ILogger<TokenRepository> logger,
    IHttpClientFactory httpClientFactory)
    : RepositoryBase(configuration, logger, httpClientFactory, null), ITokenRepository
{
    protected override string ResourceName => "admin/login";

    public async Task<TokenModel> CreateAsync()
    {
        var url = GenerateUrl();
        var input = new
        {
            username = Configuration.Username,
            password = Configuration.Password
        };
        var output = await PostAsync<ResponseModel<TokenDataDto>>(url, input);
        if (output?.Data == null)
        {
            throw new InvalidOperationException("Request did not return a valid response");
        }

        return new TokenModel
        {
            Value = output.Data.Token.Code,
            ExpiredDate = output.Data.Token.CreatedAt.Add(new TimeSpan(output.Data.Token.Expire))
        };
    }
}
