using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManagementSystemMITT.Models
{
    public static class BudgetHelper
    {

        public static double GetTotalCostForProject(ApplicationDbContext database, int projectId)
        {
            return database.Projects.Find(projectId).ProjectTasks.Sum(t => t.User.Salary * (t.EndDateTime - t.StartDateTime).TotalDays) + database.Projects.Find(projectId).User.Salary;
        }
        public static double GetTotalCostForTask(ApplicationDbContext database, int taskId)
        {
            var task = database.Tasks.Find(taskId);
            return task.User.Salary * ((task.EndDateTime - task.StartDateTime).TotalDays);
        }

        public static List<Project> GetProjectsExceedBudget(ApplicationDbContext database)
        {
            return database.Projects.Where(p => p.Budget < GetTotalCostForProject(database, p.Id)).ToList();
        }
    }
}