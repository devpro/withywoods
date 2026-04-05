using Devpro.Yanport.Client.UnitTests.Fakes;
using Withywoods.UnitTesting;

namespace Devpro.Yanport.Client.UnitTests.Repositories
{
    public abstract class RepositoryTestBase : HttpRepositoryTestBase
    {
        protected RepositoryTestBase()
            : base()
        {
            Configuration = new FakeConfiguration();
        }

        protected IYanportClientConfiguration Configuration { get; private set; }
    }
}
