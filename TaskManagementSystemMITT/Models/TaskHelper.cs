using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManagementSystemMITT.Models
{
    public class TaskHelper
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ProjectManagerId { get; set; }
        public ApplicationUser ProjectManager { get; set; }

        static ApplicationDbContext db = new ApplicationDbContext();

        public static bool CreateTask(ProjectTask task)
        {
            if (db.Tasks.Any(p => p.Name == task.Name))
            {
                return false;
            }
            task.Comment = "";
            task.IsCompleted = false;
            db.Tasks.Add(task);
            db.SaveChanges();
            return true;
        }

        public static bool DeleteTask(int id)
        {
            var task = db.Tasks.Where(i => i.Id == id).FirstOrDefault();
            if (task != null)
            {
                db.Tasks.Remove(task);
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public static bool EditTask(ProjectTask pTask)
        {
            var task = db.Tasks.Where(i => i.Id == pTask.Id).FirstOrDefault();
            if (task != null)
            {
                task.Name = pTask.Name;
                task.ProjectId = pTask.ProjectId;
                task.Description = pTask.Description;
                task.StartDateTime = pTask.StartDateTime;
                task.EndDateTime = pTask.EndDateTime;
                task.IsCompleted = pTask.IsCompleted;
                task.UserId = pTask.UserId;
                task.PercentCompleted = pTask.PercentCompleted;
                task.Comment = pTask.Comment;
                task.Priority = pTask.Priority;
                db.Entry(task).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public static ProjectTask GetTask(int id)
        {
            return db.Tasks.Where(i => i.Id == id).FirstOrDefault();
        }

        public static List<ProjectTask> GetAllTaskByUser(string Userid)
        {
            return db.Tasks.Where(i => i.User.Id == Userid).ToList();
        }

        public static bool AssignTaskToUser(ProjectTask pTask, ApplicationUser user)
        {
            var task = db.Tasks.Where(i => i.Id == pTask.Id).FirstOrDefault();
            if (task != null)
            {
                pTask.User = user;
                db.Entry(task).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public static void MarkCompleted(ApplicationDbContext database, int id)
        {
            var task = database.Tasks.Find(id);

            task.IsCompleted = true;
            database.SaveChanges();

        }


    }

}