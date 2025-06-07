using TaskManagementBackend.Models;

namespace TaskManagementBackend.Repositories.Interface
{
    public interface IUserRepository
    {
        Task<User> GetByUsernameAsync(string username);

        Task<User> CreateUserAsync(User user);

        Task<List<User>> GetAllUsersAsync();
    }
}
