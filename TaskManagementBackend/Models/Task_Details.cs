using System.ComponentModel.DataAnnotations;

namespace TaskManagementBackend.Models
{
    public class Task_Details
    {
        [Key]
        public int historyid { get; set; }
        public int taskid { get; set; }
        public string actiontaken { get; set; }
        public int updatedby { get; set; }
        public string newstatus { get; set; }
        public int? assignedto { get; set; }
        public DateTime updatedon { get; set; }
        public string Comments { get; set; }

        public virtual Tasks Task { get; set; }
        public virtual User UpdatedByNavigation { get; set; }
    }
}
