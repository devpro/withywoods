using System.Threading.Tasks;
using Withywoods.Twohire.Abstractions.Models;

namespace Withywoods.Twohire.Abstractions.Repositories;

public interface ITokenRepository
{
    Task<TokenModel> CreateAsync();
}
