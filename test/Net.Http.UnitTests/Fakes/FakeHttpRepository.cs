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

        public async Task<List<string>> FindAllAsync()
        {
            var url = "https://does.not.exist/v42/api/fakes";
            return await GetAsync<List<string>>(url);
        }
    }
}
