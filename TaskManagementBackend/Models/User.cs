namespace TaskManagementBackend.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; }

        public virtual ICollection<Tasks> TasksCreated { get; set; }
        public virtual ICollection<Task_Details> TaskUpdates { get; set; }
    }
}
