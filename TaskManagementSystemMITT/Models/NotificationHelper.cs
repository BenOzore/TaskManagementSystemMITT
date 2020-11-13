using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TaskManagementSystemMITT.Models
{
    public static class NotificationHelper
    {
        public static void CreateTasksOneDayLeftForDev(ApplicationDbContext db, string userId)
        {
            var tasks = db.Users.Find(userId).Tasks.Where(t => (t.EndDateTime - DateTime.Now).TotalDays <= 1 && !t.IsCompleted).ToList();
            var result = new List<Notification>();
            tasks.ForEach(t =>
            {
                if (
                    !db.Notifications.Any(n =>
                        n.Body == t.Name + " is one day left to due.")
                    )
                {
                    result.Add(
                        new Notification
                        {
                            Urgent = false,
                            DateTime = DateTime.Now,
                            ProjectTaskId = t.Id,
                            UserId = userId,
                            IsOpened = false,
                            Body = $"{t.Name} is one day left to due."
                        });
                }
            });
            db.Notifications.AddRange(result);
            db.SaveChanges();
        }

        public static List<Notification> GetNotificationsForUser(ApplicationDbContext db, string userId)
        {
            return db.Notifications.Where(n => n.UserId == userId).ToList();
        }

        public static void ChangeStatus(ApplicationDbContext db, string userId)
        {
            db.Notifications.Where(n => n.UserId == userId).ToList().ForEach(n => n.IsOpened = true);
            db.SaveChanges();
        }

        public static void CreateManagerNotifications(ApplicationDbContext db, string userId)
        {
            var projectsPassDeadline = db.Users.Find(userId).Projects.Where(p => p.ProjectTasks.Any(t => !t.IsCompleted && (t.EndDateTime - DateTime.Now).TotalDays <= 0)).ToList();
            var result = new List<Notification>();
            projectsPassDeadline.ForEach(p =>
            {
                if (
                    !db.Notifications.Any(n =>
                        n.Body == p.Name+ " has unfinished tasks that pass due.")
                    )
                {
                    result.Add(
                        new Notification
                        {
                            Urgent = false,
                            DateTime = DateTime.Now,
                            ProjectId = p.Id,
                            UserId = userId,
                            IsOpened = false,
                            Body = p.Name + " has unfinished tasks that pass due."
                        });
                }
            });
            db.Notifications.AddRange(result);
            result = new List<Notification>();

            var finishedTasks = db.Users.Find(userId).Projects.SelectMany(p => p.ProjectTasks.Where(t => t.IsCompleted)).ToList();
            finishedTasks.ForEach(t =>
            {
                if (
                    !db.Notifications.Any(n =>
                        n.Body == t.Name + " is finished.")
                    )
                {
                    result.Add(
                        new Notification
                        {
                            Urgent = false,
                            DateTime = DateTime.Now,
                            ProjectTaskId = t.Id,
                            UserId = userId,
                            IsOpened = false,
                            Body = t.Name + " is finished."
                        });
                }
            });
            db.Notifications.AddRange(result);
            result = new List<Notification>();

            var finishedProjects = db.Users.Find(userId).Projects.Where(p => p.ProjectTasks.All(t => t.IsCompleted)).ToList();
            finishedProjects.ForEach(p =>
            {
                if (
                    !db.Notifications.Any(n =>
                        n.Body == p.Name + " is finished.")
                    )
                {
                    result.Add(
                        new Notification
                        {
                            Urgent = false,
                            DateTime = DateTime.Now,
                            ProjectId = p.Id,
                            UserId = userId,
                            IsOpened = false,
                            Body = p.Name + " is finished."
                        });
                }
            });
            db.Notifications.AddRange(result);
            result = new List<Notification>();


            var urgentNoteTasks = db.Users.Find(userId).Projects.SelectMany(p => p.ProjectTasks.Where(t => !t.IsCompleted && t.Comment != null)).ToList();
            urgentNoteTasks.ForEach(t =>
            {
                if (
                    !db.Notifications.Any(n =>
                        n.Body == t.Name + " has an urgent note.")
                    )
                {
                    result.Add(
                        new Notification
                        {
                            Urgent = true,
                            DateTime = DateTime.Now,
                            ProjectTaskId = t.Id,
                            UserId = userId,
                            IsOpened = false,
                            Body = t.Name + " has an urgent note."
                        });
                }
            });
            db.Notifications.AddRange(result);
            db.SaveChanges();
        }
    }
}