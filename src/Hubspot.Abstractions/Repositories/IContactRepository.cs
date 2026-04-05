using System.Collections.Generic;
using System.Threading.Tasks;
using Withywoods.Hubspot.Abstractions.Models;

namespace Withywoods.Hubspot.Abstractions.Repositories;

public interface IContactRepository
{
    Task<List<ContactModel>> FindAllAsync();
}
