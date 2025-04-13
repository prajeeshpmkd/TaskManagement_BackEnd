using TaskManagementBackend.Models;

namespace TaskManagementBackend.Repositories.Interface
{
    public interface IUserRepository
    {
        Task<User> GetByUsernameAsync(string username);
    }
}
