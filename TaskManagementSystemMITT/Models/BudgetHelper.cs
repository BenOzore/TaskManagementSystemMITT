using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManagementSystemMITT.Models
{
    public static class BudgetHelper
    {
        static ApplicationDbContext db = new ApplicationDbContext();

        public static int GetTotalCostForProject(int projectId)
        {
            return db.Projects.Find(projectId).ProjectTasks.Sum(t => t.User.Salary) + db.Projects.Find(projectId).User.Salary;
        }
        public static List<Project> GetProjectsExceedBudget()
        {
            return db.Projects.Where(p => p.Budget < GetTotalCostForProject(p.Id)).ToList();
        }
    }
}