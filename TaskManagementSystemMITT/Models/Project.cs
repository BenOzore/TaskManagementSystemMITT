﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TaskManagementSystemMITT.Models
{
    public class Project
    {
        public Project()
        {
            this.ProjectTasks = new HashSet<ProjectTask>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<ProjectTask> ProjectTasks { get; set; }
        
        public virtual ApplicationUser User { get; set; }
        
        public string UserId { get; set; }

    }
}