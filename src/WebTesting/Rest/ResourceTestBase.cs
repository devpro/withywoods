using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;

namespace Withywoods.WebTesting.Rest
{
    [Obsolete("This class will soon be removed, please use RestClient class of the same namespace instead.")]
    [ExcludeFromCodeCoverage]
    /// <summary>
    /// Abstract class for tests against HTTP REST resources.
    /// </summary>
    public class ResourceTestBase : RestClient
    {
        public ResourceTestBase(HttpClient httpClient)
            : base(httpClient)
        {
        }
    }
}
