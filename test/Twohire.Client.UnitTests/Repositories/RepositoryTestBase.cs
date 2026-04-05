using Moq;
using Withywoods.Net.Http.UnitTests;
using Withywoods.Twohire.Abstractions.Providers;
using Withywoods.Twohire.Client.UnitTests.Fakes;

namespace Withywoods.Twohire.Client.UnitTests.Repositories;

public abstract class RepositoryTestBase : HttpRepositoryTestBase
{
    protected ITwohireClientConfiguration Configuration { get; private set; } = new FakeConfiguration();

    protected Mock<ITokenProvider> TokenProviderMock { get; private set; } = new();
}
