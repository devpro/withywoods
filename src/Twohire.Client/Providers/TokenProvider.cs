using System;
using Microsoft.Extensions.Logging;
using Withywoods.Twohire.Abstractions.Providers;
using Withywoods.Twohire.Abstractions.Repositories;

namespace Withywoods.Twohire.Client.Providers;

public class TokenProvider(ILogger<TokenProvider> logger, ITokenRepository tokenRepository)
    : ITokenProvider
{
    private DateTime _expirationTime;

    public string Token
    {
        get
        {
            var now = DateTime.Now;
            if (field != null && _expirationTime > now)
            {
                return field;
            }

            var tokenDto = tokenRepository.CreateAsync().GetAwaiter().GetResult();
            field = tokenDto.Value;
            _expirationTime = tokenDto.ExpiredDate;
            logger.LogDebug("New token generated");
            return field;
        }
    }
}
