using System;
using Microsoft.Extensions.Logging;
using Withywoods.Hubspot.Abstractions.Providers;
using Withywoods.Hubspot.Abstractions.Repositories;

namespace Withywoods.Hubspot.Client.Providers;

public class TokenProvider(ILogger<TokenProvider> logger, ITokenRepository tokenRepository) : ITokenProvider
{
    private DateTime _expirationTime;

    public required string AuthorizationCode { get; set; }

    public string Token
    {
        get
        {
            var now = DateTime.Now;
            if (field != null && _expirationTime > now)
            {
                return field;
            }

            var tokenDto = tokenRepository.CreateAsync(AuthorizationCode).GetAwaiter().GetResult();
            field = tokenDto.AccessToken;
            _expirationTime = tokenDto.ExpiredAt;
            logger.LogDebug("New token generated");
            return field;
        }
    }
}
