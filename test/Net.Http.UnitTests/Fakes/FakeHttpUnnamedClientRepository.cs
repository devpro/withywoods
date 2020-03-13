using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Withywoods.Net.Http.UnitTests.Fakes
{
    public class FakeHttpUnnamedClientRepository : HttpRepositoryBase
    {
        public FakeHttpUnnamedClientRepository(ILogger<FakeHttpRepository> logger, IHttpClientFactory httpClientFactory)
            : base(logger, httpClientFactory)
        {
        }

        protected override string HttpClientName => "";

        public async Task<List<string>> FindAllAsync()
        {
            var url = "https://still.not.here/api/fakes";
            return await GetAsync<List<string>>(url);
        }
    }
}
