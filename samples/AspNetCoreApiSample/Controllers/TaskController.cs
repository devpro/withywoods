using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Withywoods.AspNetCoreApiSample.Dal;
using Withywoods.AspNetCoreApiSample.Dal.Repositories;
using Withywoods.AspNetCoreApiSample.Dto;

namespace Withywoods.AspNetCoreApiSample.Controllers
{
    /// <summary>
    /// REST controller providing CRUD actions on "Task" resource.
    /// </summary>
    [Route("api/tasks")]
    [ApiController]
    [Produces("application/json")]
    public class TaskController : ControllerBase
    {
        #region Private members & Constructor

        private readonly ITaskDbContext _dbContext;
        private readonly ITaskRepository _taskRepository;

        /// <summary>
        /// Create a new instance of <see cref="TaskController"/>.
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="taskRepository"></param>
        public TaskController(ITaskDbContext dbContext, ITaskRepository taskRepository)
        {
            _dbContext = dbContext;
            _taskRepository = taskRepository;
        }

        #endregion

        #region Public actions

        /// <summary>
        /// Retrieves all tasks.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/tasks
        ///     {
        ///     }
        /// 
        /// </remarks>
        /// <returns>List of <see cref="TaskDto"/></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<TaskDto>))]
        public ActionResult<List<TaskDto>> Get()
        {
            return _taskRepository.FindAll();
        }

        /// <summary>
        /// Retrieves one task by its id.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/tasks/5
        ///     {
        ///     }
        /// 
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>One <see cref="TaskDto"/> if it exists</returns>
        [HttpGet("{id}", Name = "GetTaskById")]
        [ProducesResponseType(200, Type = typeof(TaskDto))]
        [ProducesResponseType(404)]
        public ActionResult<TaskDto> Get(string id)
        {
            var item = _dbContext.TaskItems.Find(id);
            if (item == null)
                return NotFound();

            return item;
        }

        /// <summary>
        /// Creates a task.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/tasks
        ///     {
        ///        "id": "1",
        ///        "title": "Item1",
        ///        "isComplete": true
        ///     }
        ///
        /// </remarks>
        /// <param name="item"></param>
        /// <returns>A newly created <see cref="TaskDto"/></returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>            
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public ActionResult<TaskDto> Create(TaskDto item)
        {
            item.Id = Guid.NewGuid().ToString();
            _dbContext.TaskItems.Add(item);
            _dbContext.SaveChanges();

            return CreatedAtRoute("GetTaskById", new { id = item.Id }, item);
        }

        /// <summary>
        /// Updates a task.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT api/tasks/5
        ///     {
        ///        "id": "5",
        ///        "title": "Item5",
        ///        "isComplete": true
        ///     }
        ///
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="item"></param>
        /// <returns>No content</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Update(string id, TaskDto item)
        {
            if (string.IsNullOrEmpty(item.Id) || item.Id != id)
                return BadRequest();

            var task = _dbContext.TaskItems.Find(id);
            if (task == null)
                return NotFound();

            task.Title = item.Title;
            task.IsComplete = item.IsComplete;

            _dbContext.TaskItems.Update(task);
            _dbContext.SaveChanges();

            return NoContent();
        }

        /// <summary>
        /// Deletes a specific task.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE api/tasks/5
        ///     {
        ///     }
        ///
        /// </remarks>
        /// <param name="id"></param>        
        /// <returns>No content</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult Delete(string id)
        {
            var task = _dbContext.TaskItems.Find(id);
            if (task != null)
            {
                _dbContext.TaskItems.Remove(task);
                _dbContext.SaveChanges();
            }

            return NoContent();
        }

        #endregion
    }
}
