using Microsoft.AspNetCore.Mvc;
using TaskFlow.API.DTOs;
using TaskFlow.API.Models;
using TaskFlow.API.Services;

namespace TaskFlow.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public IActionResult GetTasks()
        {
            var response = _taskService.GetAll().Select(task => new TaskResponseDto
            {
                TaskId = task.TaskId,
                ProjectId = task.ProjectId,
                Title = task.Title,
                Description = task.Description
            });

            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetTaskById([FromRoute] int id)
        {
            var task = _taskService.GetById(id);

            if (task == null) return NotFound();

            var response = new TaskResponseDto
            {
                TaskId= task.TaskId,
                ProjectId = task.ProjectId,
                Title = task.Title,
                Description = task.Description
            };

            return Ok(response);
        }

        [HttpPost]
        public IActionResult CreateTask([FromBody] CreateTaskDto dto)
        {
            var task = new TaskItem
            {
                ProjectId = dto.ProjectId,
                Title = dto.Title,
                Description = dto.Description,
                IsCompleted = false
            };

            var createdTask = _taskService.Create(task);

            var response = new TaskResponseDto
            {
                TaskId = createdTask.TaskId,
                ProjectId = task.ProjectId,
                Title = task.Title,
                Description = task.Description
            };

            return CreatedAtAction(nameof(GetTaskById), new { id = createdTask.TaskId }, response);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTask(int id, [FromBody] UpdateTaskDto dto)
        {
            var existingTask = _taskService.GetById(id);

            if (existingTask == null) return NotFound();

            existingTask.ProjectId = dto.ProjectId;
            existingTask.Title = dto.Title;
            existingTask.Description = dto.Description;
            existingTask.IsCompleted = dto.IsCompleted;

            var response = new TaskResponseDto
            {
                TaskId = existingTask.TaskId,
                ProjectId = existingTask.ProjectId,
                Title = existingTask.Title,
                Description = existingTask.Description
            };

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTask(int id)
        {
            var deleted = _taskService.Delete(id);

            if (!deleted) return NotFound();

            return NoContent();
        }
    }
}
