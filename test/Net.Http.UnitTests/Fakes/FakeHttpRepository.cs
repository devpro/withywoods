using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Withywoods.Net.Http.UnitTests.Fakes;

public class FakeHttpRepository(ILogger<FakeHttpRepository> logger, IHttpClientFactory httpClientFactory)
    : HttpRepositoryBase(logger, httpClientFactory)
{
    protected override string HttpClientName => "FakeApi";

    public async Task<List<T>?> FindAllAsync<T>()
    {
        return await GetAsync<List<T>>("https://does.not.exist/v42/api/fakes");
    }

    public async Task<T?> CreateAsync<T>(T input) where T : class
    {
        return await PostAsync<T>("https://does.not.exist/v42/api/fakes", input);
    }

    public async Task UpdateAsync<T>(string id, T input)
    {
        if (input != null)
        {
            await PutAsync($"https://does.not.exist/v42/api/fakes/{id}", input);
        }
    }

    public async Task DeleteAsync(string id)
    {
        await DeleteAsync($"https://does.not.exist/v42/api/fakes/{id}", true);
    }
}
