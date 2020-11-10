using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace TaskManagementSystemMITT.Models
{
    public static class ProjectHelper
    {
        static ApplicationDbContext db = new ApplicationDbContext();

        public static bool CreateProject(Project project)
        {
            if (db.Projects.Any(p => p.Name == project.Name))
            {
                return false;
            }
            db.Projects.Add(project);
            db.SaveChanges();
            return true;
        }

        public static bool DeleteProject(int id)
        {
            if (db.Projects.Any(p => p.Id == id))
            {
                var proj = db.Projects.Find(id);
                db.Tasks.RemoveRange(proj.ProjectTasks);
                db.Projects.Remove(proj);
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public static bool EditProject(int? id, string name)
        {
            if (db.Projects.Any(p => p.Id == id) && !db.Projects.Any(p => p.Name == name))
            {
                var proj = db.Projects.Find(id);
                proj.Name = name;
                return true;
            }
            return false;
        }

        public static List<ProjectTask> AllTasksByProject(ApplicationDbContext database, int id)
        {
            return database.Projects.First(p => p.Id == id).ProjectTasks.OrderByDescending(i=>i.PercentCompleted).ToList();
        }

        public static List<Project> AllProjectsByUser(ApplicationDbContext database, string id)
        {
            return database.Users.Find(id).Projects.ToList();
        }
    }
}