using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Withywoods.AspNetCoreApiSample.Dto
{
    /// <summary>
    /// Task DTO (Data Transfer Object).
    /// </summary>
    public class TaskDto
    {
        /// <summary>
        /// Task id.
        /// </summary>
        public string? Id { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Task title.
        /// </summary>
        [Required]
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Has the task been completed?
        /// False by default.
        /// </summary>
        [DefaultValue(false)]
        public bool IsComplete { get; set; }
    }
}
