using System.Collections.Generic;
using System.Threading.Tasks;
using Devpro.Yanport.Abstractions.Models;

namespace Devpro.Yanport.Abstractions.Repositories
{
    /// <summary>
    /// Property repository.
    /// </summary>
    public interface IPropertyRepository
    {
        /// <summary>
        /// Find all properties.
        /// </summary>
        /// <param name="queryParameters">query parameter string, beginning with a question mark (?)</param>
        /// <returns></returns>
        /// <remarks>First search criteria will be delivered in the next release of the Client</remarks>
        Task<List<HitModel>> FindAllAsync(string queryParameters = null);
    }
}
