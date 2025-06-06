using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
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
                Due_date = result.Due_date,
                Creationtimestamp = result.Creationtimestamp,
                createdby = result.createdby,
                lastmodifiedby = result.lastmodifiedby,
                lastmodifiedtimestamp=result.lastmodifiedtimestamp
            };

            return Ok(response);
        }

        [Authorize]
        [HttpGet("GetAllTasks")]
        public async Task<ActionResult> GetAllTasks()
        {
            var tasks = await taskRepository.GetAllTasksAsync();

            var response=new List<TaskDto>();

            foreach (var task in tasks)
            {
                response.Add(new TaskDto
                {
                  taskid= task.taskid,
                  TaskName = task.TaskName,
                  Description = task.Description,
                  Status = task.Status,
                  Priority = task.Priority,
                  Due_date = task.Due_date,
                  Creationtimestamp = task.Creationtimestamp,
                  createdby = task.createdby
                });
            }

            return Ok(response);
        }

        [Authorize]
        [HttpPut("{taskId}")]
        public async Task<ActionResult> UpdateTaskAsync(int taskId, [FromBody] UpdateTaskDto updateTaskDto)
        {
            if (updateTaskDto == null) 
            {
                return BadRequest("Invalid request.."); 
            }

            var result = await taskRepository.UpdateTaskAsync(taskId, updateTaskDto);

            if (result == null)
            {
                return NotFound("Task not found.");
            }

            return Ok(result);


        }
    }
}
