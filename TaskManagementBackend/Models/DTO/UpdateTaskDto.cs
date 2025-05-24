namespace TaskManagementBackend.Models.DTO
{
    public class UpdateTaskDto
    {
        public string TaskName { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
        public DateTime Creationtimestamp { get; set; }
        public int createdby { get; set; }
    }
}
