using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
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

        [HttpGet("{projectId}")]
        public ActionResult<IEnumerable<TaskDto>> GetTasksByProject(Guid projectId)
        {
            var tasks = _taskService.GetTasksByProjectId(projectId);
            return Ok(tasks);
        }

        [HttpPost]
        //public ActionResult<TaskDto> CreateTask([FromBody] TaskDto taskDto)
        //{
        //    var task = _taskService.CreateTask(
        //    taskDto.ProjectId,
        //    taskDto.Name,
        //    taskDto.Description,
        //    taskDto.DueDate,
        //    taskDto.Priority,
        //    taskDto.AssignedUserId
        //    );

        //    return CreatedAtAction(nameof(GetTasksByProject), new { projectId = task.ProjectId }, task);
        //}
        [HttpPost]
        public ActionResult<TaskDto> CreateTask([FromBody] TaskDto taskDto)
        {
            var task = _taskService.CreateTask(taskDto); // Passando o DTO diretamente
            return CreatedAtAction(nameof(GetTasksByProject), new { projectId = task.ProjectId }, task);
        }


        [HttpPut("{taskId}")]
        public IActionResult UpdateTask(Guid taskId, Domain.ValueObjects.TaskStatus taskStatus, [FromBody] TaskDto taskDto)
        {
            try
            {
                _taskService.UpdateTask(taskId, taskStatus, taskDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{taskId}")]
        public IActionResult DeleteTask(Guid taskId)
        {
            try
            {
                _taskService.DeleteTask(taskId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

