using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskFlow.API.DTOs;
using TaskFlow.API.Models;
using TaskFlow.API.Services;

namespace TaskFlow.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;


        public TaskController(ITaskService taskService, IMapper mapper, ICurrentUserService currentUserService)
        {
            _taskService = taskService;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTasks()
        {
            var tasks = await _taskService.GetAllAsync();
            var response = _mapper.Map<IEnumerable<TaskResponseDto>>(tasks);

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskById([FromRoute] int id)
        {
            var task = await _taskService.GetByIdAsync(id);

            if (task == null) return NotFound();

            var response = _mapper.Map<TaskResponseDto>(task);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] CreateTaskDto dto)
        {
            var task = _mapper.Map<TaskItem>(dto);
            task.IsCompleted = false;
            task.AssignedUserId = _currentUserService.UserId;

            var createdTask = await _taskService.CreateAsync(task);

            var response = _mapper.Map<TaskResponseDto>(createdTask);

            return CreatedAtAction(nameof(GetTaskById), new { id = createdTask.TaskId }, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, [FromBody] UpdateTaskDto dto)
        {
            var existingTask = await _taskService.GetByIdAsync(id);
            if (existingTask == null) return NotFound();

            _mapper.Map(dto, existingTask);
            var updated = await _taskService.UpdateAsync(id, existingTask);

            if(!updated) return NotFound();
            var response = _mapper.Map<TaskResponseDto>(existingTask);

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
