using Microsoft.EntityFrameworkCore;
using TaskManagementBackend.Models;

namespace TaskManagementBackend.Data
{
    public class AuthDbContext : DbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Tasks> Tasks { get; set; }
        public DbSet<Task_Details> Task_Details { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Tasks.createdby → Users.Id (creator)
            modelBuilder.Entity<Tasks>()
                .HasOne(t => t.CreatedByNavigation)
                .WithMany(u => u.TasksCreated)
                .HasForeignKey(t => t.createdby)
                .OnDelete(DeleteBehavior.Restrict);

            // Task_Details.taskid → Tasks.taskid
            modelBuilder.Entity<Task_Details>()
                .HasOne(td => td.Task)
                .WithMany(t => t.TaskDetails)
                .HasForeignKey(td => td.taskid)
                .OnDelete(DeleteBehavior.Cascade);

            // Task_Details.updatedby → Users.Id (who updated the task)
            modelBuilder.Entity<Task_Details>()
                .HasOne(td => td.UpdatedByNavigation)
                .WithMany(u => u.TaskUpdates)
                .HasForeignKey(td => td.updatedby)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
