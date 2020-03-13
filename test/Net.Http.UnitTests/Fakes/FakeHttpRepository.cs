using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Withywoods.Net.Http.UnitTests.Fakes
{
    public class FakeHttpRepository : HttpRepositoryBase
    {
        public FakeHttpRepository(ILogger<FakeHttpRepository> logger, IHttpClientFactory httpClientFactory)
            : base(logger, httpClientFactory)
        {
        }

        protected override string HttpClientName => "FakeApi";

        public async Task<List<T>> FindAllAsync<T>()
        {
            var url = "https://does.not.exist/v42/api/fakes";
            return await GetAsync<List<T>>(url);
        }

        public async Task<T> CreateAsync<T>(T input) where T : class
        {
            var url = "https://does.not.exist/v42/api/fakes";
            return await PostAsync<T>(url, input);
        }

        public async Task UpdateAsync<T>(string id, T input)
        {
            var url = $"https://does.not.exist/v42/api/fakes/{id}";
            await PutAsync(url, input);
        }

        public async Task DeleteAsync(string id)
        {
            var url = $"https://does.not.exist/v42/api/fakes/{id}";
            await DeleteAsync(url, true);
        }
    }
}
