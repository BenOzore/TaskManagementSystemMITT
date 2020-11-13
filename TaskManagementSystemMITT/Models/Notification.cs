using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TaskManagementSystemMITT.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public DateTime DateTime { get; set; }
        public bool Urgent { get; set; }
        
        public ProjectTask ProjectTask { get; set; }
        public int? ProjectTaskId { get; set; }

        public Project Project { get; set; }
        public int? ProjectId { get; set; }
        
        public virtual ApplicationUser User { get; set; }
        
        public string UserId { get; set; }
        public bool IsOpened { get; set; }
    }
}