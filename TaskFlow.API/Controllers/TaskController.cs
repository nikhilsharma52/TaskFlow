using Microsoft.AspNetCore.Mvc;
using TaskFlow.API.DTOs;
using TaskFlow.API.Models;

namespace TaskFlow.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private static List<TaskItem> tasks = new ()
        {
            new()
            {
                TaskId = 1,
                ProjectId = 1,
                Title = "Learn Controllers",
                Description = "Complete Day 11."
            },
            new()
            {
                TaskId = 2,
                ProjectId = 1,
                Title = "Learn DTOs",
                Description = "Complete Day 12."
            }
        };

        [HttpGet]
        public IActionResult GetTasks()
        {
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
        public IActionResult GetTaskById([FromRoute] int id)
        {
            var task = tasks.FirstOrDefault(t => t.TaskId == id);

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
        public IActionResult CreateProject([FromBody] CreateTaskDto dto)
        {
            var task = new TaskItem
            {
                TaskId = tasks.Max(t => t.TaskId) + 1,
                ProjectId = tasks.Max(p => p.ProjectId) + 1,
                Title = dto.Title,
                Description = dto.Description
            };

            tasks.Add(task);

            var response = new TaskResponseDto
            {
                ProjectId = task.ProjectId,
                Title = task.Title,
                Description = task.Description
            };

            return CreatedAtAction(nameof(GetTaskById), new { id = task.TaskId }, response);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTask(int id, [FromBody] UpdateTaskDto dto)
        {
            var task = tasks.FirstOrDefault(p => p.ProjectId == id);

            if (task == null) return NotFound();

            task.ProjectId = dto.ProjectId;
            task.Title = dto.Title;
            task.Description = dto.Description;

            var response = new TaskResponseDto
            {
                TaskId = task.TaskId,
                ProjectId = task.ProjectId,
                Title = task.Title,
                Description = task.Description
            };

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTask(int id)
        {
            var task = tasks.FirstOrDefault(p => p.TaskId == id);

            if (task == null) return NotFound();

            tasks.Remove(task);
            return NoContent();
        }
    }
}
