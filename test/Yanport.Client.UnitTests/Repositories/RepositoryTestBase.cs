using Withywoods.Net.Http.UnitTests;

namespace Withywoods.Yanport.Client.UnitTests.Repositories;

public abstract class RepositoryTestBase : HttpRepositoryTestBase
{
    protected IYanportClientConfiguration Configuration { get; private set; } = new FakeConfiguration();
}
