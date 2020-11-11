using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace TaskManagementSystemMITT.Models
{
    public static class BudgetHelper
    {

        public static double GetTotalCostForProject(ApplicationDbContext database, int projectId)
        { 
            return database.Projects.Find(projectId).ProjectTasks.Sum(t => t.User.Salary) + database.Projects.Find(projectId).User.Salary;
        }
        public static double GetTotalCostForTask(ApplicationDbContext database, int taskId)
        {
            var task = database.Tasks.Find(taskId);
            return task.User.Salary;
        }

        public static List<Project> GetProjectsExceedBudget(ApplicationDbContext database)
        {
            
            return database.Projects.Where(p => p.Budget < (p.ProjectTasks.Sum(t => t.User.Salary) + p.User.Salary)).ToList();
        }
    }
}