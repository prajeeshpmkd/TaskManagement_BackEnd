namespace TaskManagementBackend.Models.DTO
{
    public class UpdateTaskDto
    {
        public string TaskName { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
        public string Status { get; set; }
        public DateOnly? Due_date { get; set; }
        public DateOnly? Completed_date { get; set; }
        public int lastmodifiedby { get; set; }
        public DateTime lastmodifiedtimestamp { get; set; }
        public string actiontaken { get; set; }
        public int updatedby { get; set; }
        public string newstatus { get; set; }
        public int? assignedto { get; set; }
        public DateTime updatedon { get; set; }
        public string Comments { get; set; }
    }
}




