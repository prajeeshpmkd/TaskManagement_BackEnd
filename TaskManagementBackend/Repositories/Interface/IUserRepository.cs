using TaskManagementBackend.Models;

namespace TaskManagementBackend.Repositories.Interface
{
    public interface IUserRepository
    {
        Task<User> GetByUsernameAsync(string username);

        Task<bool> CreateUserAsync(User user);

        Task<List<User>> GetAllUsersAsync();
    }
}
