using Microsoft.EntityFrameworkCore;
using Withywoods.AspNetCoreApiSample.Dto;

namespace Withywoods.AspNetCoreApiSample.Dal
{
    /// <summary>
    /// Task database context interface.
    /// </summary>
    public interface ITaskDbContext
    {
        /// <summary>
        /// Task items.
        /// </summary>
        DbSet<TaskDto> TaskItems { get; set; }

        /// <summary>
        /// Save changes.
        /// </summary>
        /// <returns></returns>
        int SaveChanges();
    }
}
