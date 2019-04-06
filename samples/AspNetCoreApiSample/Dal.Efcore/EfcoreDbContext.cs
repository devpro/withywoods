using Microsoft.EntityFrameworkCore;
using Withywoods.AspNetCoreApiSample.Dto;

namespace Withywoods.AspNetCoreApiSample.Dal.Efcore
{
    /// <summary>
    /// EntityFrameworkCore database context for Todo elements.
    /// </summary>
    public class EfcoreDbContext : DbContext, ITaskDbContext
    {
        /// <summary>
        /// Create a new instance of <see cref="EfcoreDbContext"/>.
        /// </summary>
        /// <param name="options"></param>
        public EfcoreDbContext(DbContextOptions options)
            : base(options)
        {
        }

        /// <summary>
        /// Database sets for Todo elements.
        /// </summary>
        public DbSet<TaskDto> TaskItems { get; set; }
    }
}
