using System.Collections.Generic;

namespace Withywoods.AspNetCoreApiSample.IntegrationTests.Entities
{
    /// <summary>
    /// Light version of <see cref="Microsoft.AspNetCore.Mvc.ValidationProblemDetails"/>.
    /// </summary>
    public class LightValidationProblemDetails
    {
        public string Type { get; set; }
        public string Title { get; set; }
        public int? Status { get; set; }
        public string Detail { get; set; }
        public IDictionary<string, string[]> Errors { get; private set; }

        public LightValidationProblemDetails()
        {
            Errors = new Dictionary<string, string[]>();
        }
    }
}
