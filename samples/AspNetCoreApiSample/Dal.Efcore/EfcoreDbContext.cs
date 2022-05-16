using Withywoods.AspNetCoreApiSample.Dto;

namespace Withywoods.AspNetCoreApiSample.Dal.Efcore
{
    /// <summary>
    /// EntityFrameworkCore database context for tasks.
    /// </summary>
    public class EfcoreDbContext : DbContext, ITaskDbContext
    {
        /// <summary>
        /// Create a new instance of <see cref="EfcoreDbContext"/>.
        /// </summary>
        /// <param name="options"></param>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public EfcoreDbContext(DbContextOptions options)
#pragma warning restore CS8618
            : base(options)
        {
        }

        /// <summary>
        /// Database sets for tasks.
        /// </summary>
        public DbSet<TaskDto> TaskItems { get; set; }
    }
}
