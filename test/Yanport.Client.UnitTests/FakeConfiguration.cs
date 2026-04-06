using System.Diagnostics.CodeAnalysis;

namespace Withywoods.Yanport.Client.UnitTests
{
    public class FakeConfiguration : DefaultYanportClientConfiguration
    {
        [SetsRequiredMembers]
        public FakeConfiguration()
        {
            BaseUrl = "http://doesnotexist.nop";
            Token = "someuselessstring";
            HttpClientName = "MyFakeClient";
        }
    }
}
