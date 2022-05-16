using Withywoods.AspNetCoreApiSample.Utils;

namespace Withywoods.AspNetCoreApiSample.Dto
{
    /// <summary>
    /// Task Patch DTO (Data Transfer Object).
    /// </summary>
    public class TaskPatchDto : PropertiesChangedTracker
    {
        private string _id = string.Empty;

        private string _title = string.Empty;

        private bool _isComplete;

        /// <summary>
        /// Task id.
        /// </summary>
        public string Id { get { return _id; } set { OnPropertyChanged(); _id = value; } }

        /// <summary>
        /// Task title.
        /// </summary>
        public string Title { get { return _title; } set { OnPropertyChanged(); _title = value; } }

        /// <summary>
        /// Has the task been completed?
        /// </summary>
        public bool IsComplete { get { return _isComplete; } set { OnPropertyChanged(); _isComplete = value; } }
    }
}
