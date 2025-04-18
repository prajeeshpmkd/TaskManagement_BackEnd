using Microsoft.EntityFrameworkCore;
using TaskManagementBackend.Data;
using TaskManagementBackend.Models;
using TaskManagementBackend.Repositories.Interface;

namespace TaskManagementBackend.Repositories.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly AuthDbContext dbcontext;

        public UserRepository(AuthDbContext _dbcontext)
        {
                dbcontext = _dbcontext;
        }

        public async Task<bool> CreateUserAsync(User user)
        {
            dbcontext.Users.Add(user);
            return await dbcontext.SaveChangesAsync() > 0;
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            return await dbcontext.Users.SingleOrDefaultAsync(u => u.Username == username);
        }
    }
}
