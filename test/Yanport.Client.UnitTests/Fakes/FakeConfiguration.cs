namespace Devpro.Yanport.Client.UnitTests.Fakes
{
    public class FakeConfiguration : DefaultYanportClientConfiguration
    {
        public FakeConfiguration()
        {
            BaseUrl = "http://doesnotexist.nop";
            Token = "someuselessstring";
            HttpClientName = "MyFakeClient";
        }
    }
}
