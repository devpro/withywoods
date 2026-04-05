using System.Collections.Generic;
using System.Threading.Tasks;
using Withywoods.Twohire.Abstractions.Models;

namespace Withywoods.Twohire.Abstractions.Repositories;

public interface IPersonalVehicleRepository
{
    Task<ResponseModel<List<object>>> FindAllAsync();
}
