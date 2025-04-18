using TaskManagementBackend.Models;
using TaskManagementBackend.Models.DTO;

namespace TaskManagementBackend.Repositories.Interface
{
    public interface ITaskRepository
    {
        Task<Tasks> CreateTaskAsync(CreateTaskDto createTaskDto);
    }
}
