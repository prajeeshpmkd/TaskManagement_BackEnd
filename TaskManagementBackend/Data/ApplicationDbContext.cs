using Microsoft.EntityFrameworkCore;

namespace TaskManagementBackend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options) { }
        
    }
}
