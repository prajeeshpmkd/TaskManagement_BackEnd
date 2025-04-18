namespace TaskManagementBackend.Models.DTO
{
    public class TaskDto
    {
        public int taskid { get; set; }
        public string TaskName { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string Priority { get; set; }
        public DateTime Creationtimestamp { get; set; }
        public int createdby { get; set; }

    }
}
