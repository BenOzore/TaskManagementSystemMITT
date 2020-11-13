namespace TaskManagementSystemMITT.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.UI.WebControls;
    using TaskManagementSystemMITT.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<TaskManagementSystemMITT.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TaskManagementSystemMITT.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.
            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            context.Database.ExecuteSqlCommand("sp_MSForEachTable 'ALTER TABLE ? NOCHECK CONSTRAINT ALL'");
            context.Database.ExecuteSqlCommand("sp_MSForEachTable 'IF OBJECT_ID(''?'') NOT IN (ISNULL(OBJECT_ID(''[dbo].[__MigrationHistory]''),0)) DELETE FROM ?'");
            context.Database.ExecuteSqlCommand("EXEC sp_MSForEachTable 'ALTER TABLE ? CHECK CONSTRAINT ALL'");

            /*Roles for Developer and Manager*/
            if (!context.Roles.Any(r => r.Name == "Developer"))/*Developer Role*/
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Developer" };
                manager.Create(role);
            }
            if (!context.Roles.Any(r => r.Name == "Manager"))/*Manager Role*/
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Manager" };

                manager.Create(role);
            }

            /*------------------------------------------------------------------------------------------------------*/
            /*Creating and adding one user to each role*/
            var PasswordHash = new PasswordHasher();
            var user1 = new ApplicationUser
            {
                UserName = "manager@mysite.com",
                Email = "manager@mysite.com",
                PasswordHash = PasswordHash.HashPassword("123456"),
                Salary = 30
            };
            if (!context.Users.Any(u => u.UserName == "manager@mysite.com"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);                                
                manager.Create(user1);
                manager.AddToRole(user1.Id, "Manager");
            }
            
            var user2 = new ApplicationUser
            {
                UserName = "developer@mysite.com",
                Email = "developer@mysite.com",
                PasswordHash = PasswordHash.HashPassword("123456"),
                Salary = 20
            };
            if (!context.Users.Any(u => u.UserName == "developer@mysite.com"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);               
                manager.Create(user2);
                manager.AddToRole(user2.Id, "Developer");
            }
            /*------------------------------------------------------------------------------------------------------*/
            /*Creating a Project and adding User1 to these tasks*/
            Project[] projects =
            {
                new Project
                {
                    Id = 1,
                    Name = "TaskManagementSystemtMITT",
                    Description = "Task management system with developers and project managers used to manage simple projects.",
                    DueDate = new DateTime(2020,11,13),
                    UserId = user1.Id,
                    Priority = Priority.Low,
                    Budget = 10
                },
                new Project
                {
                    Id = 2,
                    Name = "TrelloManagementSystem",
                    Description = "Online task management system with numerous templates",
                    DueDate = new DateTime(2020,11,04),
                    UserId = user1.Id,
                    Priority = Priority.High,
                    Budget = 300
                }
            };
            context.Projects.AddOrUpdate(t => t.Name, projects);

            /*------------------------------------------------------------------------------------------------------*/
            /*Creating two Tasks and adding User2 to these tasks*/
            ProjectTask[] tasks =
            {
                new ProjectTask
                {
                    Id = 1,
                    Name = "Seed method",
                    Description = "Writing seed method for task management system",
                    StartDateTime = DateTime.Parse("06/11/2020", new System.Globalization.CultureInfo("pt-BR")),
                    EndDateTime = DateTime.Parse("13/11/2020", new System.Globalization.CultureInfo("pt-BR")),
                    IsCompleted = false,
                    Priority = Priority.Low,
                    UserId = user2.Id,
                    ProjectId = 1,
                },
                new ProjectTask
                {
                    Id = 2,
                    Name = "Project Helper class",
                    Description = "Create Project helper class",
                    StartDateTime = DateTime.Parse("05/11/2020", new System.Globalization.CultureInfo("pt-BR")),
                    EndDateTime = DateTime.Parse("15/11/2020", new System.Globalization.CultureInfo("pt-BR")),
                    IsCompleted = false,
                    Priority = Priority.High,
                    UserId = user2.Id,
                    ProjectId = 2,
                },
                new ProjectTask
                {
                    Id = 3,
                    Name = "Task Helper method",
                    Description = "Creating task helper classes and methods",
                    StartDateTime = DateTime.Parse("05/11/2020", new System.Globalization.CultureInfo("pt-BR")),
                    EndDateTime = DateTime.Parse("11/11/2020", new System.Globalization.CultureInfo("pt-BR")),
                    IsCompleted = true,
                    Priority = Priority.Low,
                    UserId = user2.Id,
                    ProjectId = 2,
                },
                 new ProjectTask
                 {
                     Id = 4,
                    Name = "Project Helper view",
                    Description = "Creating project helper controller and view for task management system",
                    StartDateTime = DateTime.Parse("04/11/2020", new System.Globalization.CultureInfo("pt-BR")),
                    EndDateTime = DateTime.Parse("09/11/2020", new System.Globalization.CultureInfo("pt-BR")),
                    IsCompleted = true,
                    Priority = Priority.Medium,
                    UserId = user2.Id,
                    ProjectId = 2,
                 }
            };
            context.Tasks.AddOrUpdate(t => t.Description, tasks);
        }
    }
}
