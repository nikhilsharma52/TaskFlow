using Microsoft.AspNetCore.Mvc;
using TaskFlow.API.DTOs;
using TaskFlow.API.Models;

namespace TaskFlow.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController : ControllerBase
    {

        private static List<Project> projects = new List<Project>
        {
            new()
            {
                ProjectId = 1,
                Name = "TaskFlow",
                Description = "Task Management System"
            },
            new()
            {
                ProjectId = 2,
                Name = "Library",
                Description = "Library Management System"
            }
        };

        [HttpGet]
        public IActionResult GetProjects()
        {
            var response = projects.Select(project => new ProjectResponseDto
            {
                ProjectId = project.ProjectId,
                Name = project.Name,
                Description = project.Description
            });

            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetProjectById([FromRoute] int id)
        {
            var project = projects.FirstOrDefault(p => p.ProjectId == id);

            if (project == null) return NotFound();

            var response = new ProjectResponseDto
            {
                ProjectId = project.ProjectId,
                Name = project.Name,
                Description = project.Description
            };

            return Ok(response);
        }

        [HttpGet("search")]
        public IActionResult SearchProjects([FromQuery] string name)
        {
            var matchingProjects = projects.Where(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
            if (!matchingProjects.Any()) return NotFound();
            return Ok(matchingProjects);
        }

        [HttpPost]
        public IActionResult CreateProject([FromBody]  CreateProjectDto dto)
        {
            var project = new Project
            {
                ProjectId = projects.Max(p => p.ProjectId) + 1,
                Name = dto.Name,
                Description = dto.Description
            };

            projects.Add(project);

            var response = new ProjectResponseDto
            {
                ProjectId = project.ProjectId,
                Name = project.Name,
                Description = project.Description
            };

            return CreatedAtAction(nameof(GetProjectById), new { id = project.ProjectId }, response);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProject(int id, [FromBody] UpdateProjectDto dto)
        {
            var project = projects.FirstOrDefault(p => p.ProjectId == id);

            if (project == null) return NotFound();

            project.Name = dto.Name;
            project.Description = dto.Description;

            var response = new ProjectResponseDto
            {
                ProjectId = project.ProjectId,
                Name = project.Name,
                Description = project.Description
            };

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProject(int id)
        {
            var project = projects.FirstOrDefault(p => p.ProjectId == id);

            if (project == null) return NotFound();

            projects.Remove(project);
            return NoContent();
        }
    }
}
