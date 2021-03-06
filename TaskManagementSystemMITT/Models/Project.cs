﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TaskManagementSystemMITT.Models
{
    public class Project
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public Project()
        {
            ProjectTasks = new HashSet<ProjectTask>();
            Notifications = new HashSet<Notification>();
        }
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<ProjectTask> ProjectTasks { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        
        public virtual ApplicationUser User { get; set; }
        
        public string UserId { get; set; }
        [Required]
        public DateTime DueDate { get; set; }

        public Priority Priority { get; set; }
        [Required]
        public int Budget { get; set; }
        public double GetCost(int id)
        {
            return BudgetHelper.GetTotalCostForProject(db, id);
        }
        public string GetProgress(int id)
        {
            return ProjectHelper.GetProjectProgress(db, id);
        }

        public string GetDevelopers(int id)
        {
            return ProjectHelper.GetAllUsersForProject(db, id);
        }


    }

    
}