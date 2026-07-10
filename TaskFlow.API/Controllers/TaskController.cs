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
        public async Task<IActionResult> GetTasks()
        {
            var tasks = await _taskService.GetAllAsync();
            var response = tasks.Select(task => new TaskResponseDto
            {
                TaskId = task.TaskId,
                ProjectId = task.ProjectId,
                Title = task.Title,
                Description = task.Description
            });

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskById([FromRoute] int id)
        {
            var task = await _taskService.GetByIdAsync(id);

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
        public async Task<IActionResult> CreateTask([FromBody] CreateTaskDto dto)
        {
            var task = new TaskItem
            {
                ProjectId = dto.ProjectId,
                Title = dto.Title,
                Description = dto.Description,
                IsCompleted = false
            };

            var createdTask = await _taskService.CreateAsync(task);

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
        public async Task<IActionResult> UpdateTask(int id, [FromBody] UpdateTaskDto dto)
        {
            var existingTask = await _taskService.GetByIdAsync(id);

            if (existingTask == null) return NotFound();

            existingTask.ProjectId = dto.ProjectId;
            existingTask.Title = dto.Title;
            existingTask.Description = dto.Description;
            existingTask.IsCompleted = dto.IsCompleted;

            var updated = await _taskService.UpdateAsync(id, existingTask);

            if(!updated) return NotFound();

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
        public async Task<IActionResult> DeleteTask(int id)
        {
            var deleted = await _taskService.DeleteAsync(id);

            if (!deleted) return NotFound();

            return NoContent();
        }
    }
}
