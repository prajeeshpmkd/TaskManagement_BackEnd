using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
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
                Creationtimestamp=createTaskDto.Creationtimestamp,
                createdby=createTaskDto.createdby
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

    }
}
