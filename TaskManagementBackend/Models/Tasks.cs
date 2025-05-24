using System.ComponentModel.DataAnnotations;

namespace TaskManagementBackend.Models
{
    public class Tasks
    {
        [Key]
        public int taskid { get; set; }
        public string TaskName { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string Priority { get; set; }
        public DateOnly? Due_date { get; set; }
        public DateOnly? Completed_date { get; set; }
        public DateTime Creationtimestamp { get; set; }
        public int createdby { get; set; }

        public virtual User CreatedByNavigation { get; set; }
        public virtual ICollection<Task_Details> TaskDetails { get; set; }
    }
}
