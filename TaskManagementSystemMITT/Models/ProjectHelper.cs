using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManagementSystemMITT.Models
{
    public static class ProjectHelper
    {
        static ApplicationDbContext db = new ApplicationDbContext();

        public static bool CreateProject(string name, string projectManagerId, DateTime dueDate)
        {
            if (db.Projects.Any(p => p.Name == name))
            {
                return false;
            }
            db.Projects.Add(new Project() { Name = name, UserId = projectManagerId, DueDate = dueDate});
            db.SaveChanges();
            return true;
        }

        public static bool DeleteProject(int id)
        {
            if (db.Projects.Any(p => p.Id == id))
            {
                var proj = db.Projects.Find(id);
                proj.ProjectTasks.ForEach(t => db.Tasks.Remove(t));
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

        public static List<ProjectTask> AllTasksByProject(int id)
        {
            return db.Projects.First(p=>p.Id == id).ProjectTasks.ToList();
        }

        public static List<Project> AllProjectsByUser(string id)
        {
            return db.Users.Find(id).Projects.ToList();
        }
    }
}