using System.Threading.Tasks;
using Withywoods.Hubspot.Abstractions.Models;

namespace Withywoods.Hubspot.Abstractions.Repositories;

public interface ITokenRepository
{
    Task<TokenModel> CreateAsync(string authorizationCode);
}
