using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Withywoods.Net.Http.UnitTests.Fakes;

public class FakeHttpUnnamedClientRepository(ILogger<FakeHttpRepository> logger, IHttpClientFactory httpClientFactory)
    : HttpRepositoryBase(logger, httpClientFactory)
{
    protected override string HttpClientName => "";

    public async Task<List<string>?> FindAllAsync()
    {
        return await GetAsync<List<string>>("https://still.not.here/api/fakes");
    }
}
