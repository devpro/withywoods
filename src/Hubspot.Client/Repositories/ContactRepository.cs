using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Withywoods.Hubspot.Abstractions.Models;
using Withywoods.Hubspot.Abstractions.Providers;
using Withywoods.Hubspot.Abstractions.Repositories;

namespace Withywoods.Hubspot.Client.Repositories
{
    public class ContactRepository(
        IHubspotClientConfiguration configuration,
        ILogger<ContactRepository> logger,
        IHttpClientFactory httpClientFactory,
        ITokenProvider? tokenProvider)
        : RepositoryBase(configuration, logger, httpClientFactory, tokenProvider), IContactRepository
    {
        protected override string ResourceName => "contacts/v1/lists/all/contacts/all";

        /// <summary>
        /// Get all contacts
        /// </summary>
        /// <returns></returns>
        /// <remarks>https://developers.hubspot.com/docs/methods/contacts/get_contacts</remarks>
        public async Task<List<ContactModel>> FindAllAsync()
        {
            var url = GenerateUrl();
            var output = await GetAsync<ContactResultModel>(url);
            return output?.Contacts ?? [];
        }
    }
}
