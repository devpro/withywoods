using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Withywoods.Net.Http.UnitTests.Fakes;

public abstract class FakeHttpMessageHandler : HttpMessageHandler
{
    public virtual HttpResponseMessage Send(HttpRequestMessage request)
    {
        throw new NotImplementedException($"This code shouldn't be executed, the {request.Method} call to {request.RequestUri} must be mocked.");
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        return Task.FromResult(Send(request));
    }
}
