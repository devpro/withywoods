using System.Collections.Generic;
using Withywoods.AspNetCoreApiSample.Dto;

namespace Withywoods.AspNetCoreApiSample.Dal.Repositories
{
    /// <summary>
    /// Data repository for <see cref="TaskDto"/>.
    /// </summary>
    public interface ITaskRepository
    {
        /// <summary>
        /// Find all tasks.
        /// </summary>
        /// <returns></returns>
        List<TaskDto> FindAll();
    }
}
