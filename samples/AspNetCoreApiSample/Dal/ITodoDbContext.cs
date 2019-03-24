using Microsoft.EntityFrameworkCore;
using Withywoods.AspNetCoreApiSample.Dto;

namespace Withywoods.AspNetCoreApiSample.Dal
{
    /// <summary>
    /// Todo database context interface.
    /// </summary>
    public interface ITodoDbContext
    {
        /// <summary>
        /// Todo items.
        /// </summary>
        DbSet<TaskDto> TodoItems { get; set; }

        /// <summary>
        /// Save changes.
        /// </summary>
        /// <returns></returns>
        int SaveChanges();
    }
}
