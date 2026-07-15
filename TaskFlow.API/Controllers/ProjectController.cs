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
    public class ProjectController : ControllerBase
    {

        private readonly IProjectService _projectService;
        private readonly IMapper _mapper;

        public ProjectController(IProjectService projectService, IMapper mapper)
        {
            _projectService = projectService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetProjects()
        {
            var projects = await _projectService.GetAllAsync();
            var response = _mapper.Map<IEnumerable<ProjectResponseDto>>(projects);

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProjectById([FromRoute] int id)
        {
            var project = await _projectService.GetByIdAsync(id);

            if (project == null) return NotFound();

            var response = _mapper.Map<ProjectResponseDto>(project);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProject([FromBody]  CreateProjectDto dto)
        {
            var project = _mapper.Map<Project>(dto);

            var createdProject = await _projectService.CreateAsync(project);

            var response = _mapper.Map<ProjectResponseDto>(createdProject);

            return CreatedAtAction(nameof(GetProjectById), new { id = createdProject.ProjectId }, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(int id, [FromBody] UpdateProjectDto dto)
        {
            var existingProject = await _projectService.GetByIdAsync(id);
            if (existingProject == null) return NotFound();

            _mapper.Map(dto, existingProject);
            var updated = await _projectService.UpdateAsync(id, existingProject);

            if (!updated) return NotFound();
            var response = _mapper.Map<ProjectResponseDto>(existingProject);

            return Ok(response);
        
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var deleted = await _projectService.DeleteAsync(id);

            if (!deleted) return NotFound();

            return NoContent();
        }
    }
}
