﻿using System.ComponentModel;
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
        public string Id { get; set; }

        /// <summary>
        /// Task title.
        /// </summary>
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// Has the task been completed?
        /// False by default.
        /// </summary>
        [DefaultValue(false)]
        public bool IsComplete { get; set; }
    }
}
