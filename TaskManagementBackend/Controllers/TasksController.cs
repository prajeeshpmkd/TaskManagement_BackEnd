using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TaskManagementBackend.Models;
using TaskManagementBackend.Models.DTO;
using TaskManagementBackend.Repositories.Interface;

namespace TaskManagementBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskRepository taskRepository;
        public TasksController(ITaskRepository _taskRepository)
        {
            taskRepository = _taskRepository;
        }

        [Authorize]
        [HttpPost("NewTask")]
        public async Task<ActionResult> CreateTask([FromBody] CreateTaskDto createTaskDto)
        {
            if(createTaskDto == null)
            {
                return BadRequest("Task Creation failed..");
            }

            var result = await taskRepository.CreateTaskAsync(createTaskDto);
            var response = new TaskDto
            {
                taskid = result.taskid,
                TaskName = result.TaskName,
                Description = result.Description,
                Status = result.Status,
                Priority = result.Priority,
                Creationtimestamp = result.Creationtimestamp,
                createdby = createTaskDto.createdby
            };

            return Ok(response);
        }
    }
}
