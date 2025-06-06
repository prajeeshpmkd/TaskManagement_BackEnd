using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using TaskManagementBackend.Data;
using TaskManagementBackend.Models;
using TaskManagementBackend.Models.DTO;
using TaskManagementBackend.Repositories.Interface;

namespace TaskManagementBackend.Repositories.Implementation
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AuthDbContext authDbContext;
        public TaskRepository(AuthDbContext _authDbContext)
        {
            authDbContext= _authDbContext;
        }

        public async Task<Tasks> CreateTaskAsync(CreateTaskDto createTaskDto)
        {
            var Tasks = new Tasks
            {
                TaskName=createTaskDto.TaskName,
                Description=createTaskDto.Description,
                Status= "Open",
                Priority=createTaskDto.Priority,
                Due_date=createTaskDto.Due_date,
                Creationtimestamp =createTaskDto.Creationtimestamp,
                createdby=createTaskDto.createdby,
                lastmodifiedby = createTaskDto.createdby,
                lastmodifiedtimestamp = createTaskDto.Creationtimestamp

            };

            await authDbContext.Tasks.AddAsync(Tasks);
            await authDbContext.SaveChangesAsync();


            var Task_Details = new Task_Details
            {
                taskid = Tasks.taskid,
                actiontaken = "Created",
                updatedby = createTaskDto.createdby,
                newstatus = "Open",
                assignedto = null,
                updatedon = createTaskDto.Creationtimestamp
            };

            await authDbContext.Task_Details.AddAsync(Task_Details);
            await authDbContext.SaveChangesAsync();

            return Tasks;
        }

        public async Task<List<Tasks>> GetAllTasksAsync()
        {
                return await authDbContext.Tasks.Select(t => new Tasks
                {
                    taskid = t.taskid,
                    TaskName = t.TaskName,
                    Description = t.Description,
                    Status = t.Status,
                    Priority=t.Priority,
                    Due_date=t.Due_date,
                    Completed_date=t.Completed_date,
                    Creationtimestamp=t.Creationtimestamp,
                    createdby=t.createdby
                }).ToListAsync();
        }

        public async Task<UpdateTaskResponseDto> UpdateTaskAsync(int taskId, UpdateTaskDto updateTaskDto)
        {
            var task =await authDbContext.Tasks.FindAsync(taskId);

            if (task == null)
            {
                throw new KeyNotFoundException($"Task with ID {taskId} not found.");
            }
            task.Status = updateTaskDto.Status;
            task.Priority = updateTaskDto.Priority;
            task.Due_date = updateTaskDto.Due_date;
            task.Completed_date = updateTaskDto.Completed_date;
            task.lastmodifiedby = updateTaskDto.lastmodifiedby;
            task.lastmodifiedtimestamp = updateTaskDto.lastmodifiedtimestamp;

            var Task_Details = new Task_Details
            {
                taskid = task.taskid,
                actiontaken = updateTaskDto.actiontaken,
                updatedby = updateTaskDto.updatedby,
                newstatus = updateTaskDto.newstatus,
                assignedto = updateTaskDto.assignedto,
                updatedon = updateTaskDto.updatedon,
                Comments = updateTaskDto.Comments
            };

            await authDbContext.Task_Details.AddAsync(Task_Details);
            await authDbContext.SaveChangesAsync(); 

            var updateTaskResponseDto = new UpdateTaskResponseDto
            {
                taskid = task.taskid,
                TaskName = task.TaskName,
                Description = task.Description,
                Status = task.Status,
                Priority = task.Priority,
                Due_date = task.Due_date,
                Completed_date = task.Completed_date,
                Creationtimestamp = task.Creationtimestamp,
                createdby = task.createdby,
                lastmodifiedby = updateTaskDto.lastmodifiedby,
                lastmodifiedtimestamp = updateTaskDto.lastmodifiedtimestamp,

                historyid = Task_Details.historyid,
                actiontaken = Task_Details.actiontaken,
                updatedby = Task_Details.updatedby,
                newstatus = Task_Details.newstatus,
                assignedto = Task_Details.assignedto,
                updatedon = Task_Details.updatedon,
                Comments = Task_Details.Comments
            };
            return updateTaskResponseDto;

        }
    }
}
