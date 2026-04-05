using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Withywoods.Twohire.Abstractions.Models;
using Withywoods.Twohire.Abstractions.Providers;
using Withywoods.Twohire.Abstractions.Repositories;

namespace Withywoods.Twohire.Client.Repositories;

public class PersonalVehicleRepository(
    ITwohireClientConfiguration configuration,
    ILogger<PersonalVehicleRepository> logger,
    IHttpClientFactory httpClientFactory,
    ITokenProvider tokenProvider)
    : RepositoryBase(configuration, logger, httpClientFactory, tokenProvider), IPersonalVehicleRepository
{
    protected override string ResourceName => "admin/api/personal/vehicle";

    public async Task<ResponseModel<List<object>>> FindAllAsync()
    {
        var url = GenerateUrl();
        return await GetAsync<ResponseModel<List<object>>>(url) ?? new ResponseModel<List<object>>();
    }
}
