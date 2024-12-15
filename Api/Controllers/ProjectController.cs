using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet("{userId}")]
        public ActionResult<IEnumerable<ProjectDto>> GetAllProjects(Guid userId)
        {
            var projects = _projectService.GetAllProjects(userId);
            return Ok(projects);
        }

        [HttpGet("details/{projectId}")]
        public ActionResult<ProjectDto> GetProjectById(Guid projectId)
        {
            try
            {
                var project = _projectService.GetProjectById(projectId);
                return Ok(project);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult<ProjectDto> CreateProject([FromBody] string projectName, Guid userId)
        {
            var project = _projectService.CreateProject(projectName, userId);
            return CreatedAtAction(nameof(GetProjectById), new { projectId = project.Id }, project);
        }


        [HttpDelete("{projectId}")]
        public IActionResult DeleteProject(Guid projectId)
        {
            try
            {
                _projectService.DeleteProject(projectId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
