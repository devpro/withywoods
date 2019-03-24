using Withywoods.AspNetCoreApiSample.Dal.Repositories;

namespace Withywoods.AspNetCoreApiSample.Dal.Efcore.Repositories
{
    /// <summary>
    /// Repository for <see cref="Dto.TaskDto"/> entities.
    /// </summary>
    public class TaskRepository : ITaskRepository
    {
        private readonly EfcoreDbContext _dbContext;

        /// <summary>
        /// Create a new instance of <see cref="TaskRepository"/>.
        /// </summary>
        /// <param name="dbContext"></param>
        public TaskRepository(EfcoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
