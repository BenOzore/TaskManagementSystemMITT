using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace TaskManagementSystemMITT.Models
{
    public static class UserRoleHelper
    {
        static ApplicationDbContext db = new ApplicationDbContext();
        static UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>
            (new UserStore<ApplicationUser>(db));
        static RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>
            (new RoleStore<IdentityRole>(db));

        public static void AddRole(string roleName)
        {
            if (!roleManager.RoleExists(roleName))
                roleManager.Create(new IdentityRole { Name = roleName });
        }

        public static void DeleteRole(string roleName)
        {
            if (roleManager.RoleExists(roleName))
            {
                var role = roleManager.FindByName(roleName);
                roleManager.Delete(role);
            }
        }

        public static void UpdateRole(string userId, string roleName)
        {
            var user = userManager.FindById(userId);
            var currentUserRole = Roles.GetRolesForUser(user.UserName).ToString();

            if (!roleManager.RoleExists(roleName))
                roleManager.Create(new IdentityRole { Name = roleName });

            userManager.RemoveFromRole(userId, currentUserRole);
            userManager.AddToRole(userId, roleName);
        }

        public static void AddUserToRole(string userId, string roleName)
        {
            if (!userManager.IsInRole(userId, roleName))
                userManager.AddToRole(userId, roleName);
        }

        public static bool CheckIfUserInRole(string userId, string roleName)
        {
            var result = userManager.IsInRole(userId, roleName);

            return result;
        }

        public static ICollection<ApplicationUser> AllUsersInRole(string roleName)
        {
            var role = roleManager.Roles.Where(i => i.Name == roleName).FirstOrDefault();
            if (role==null)
            {
                return null;
            }
;            var result = userManager.Users.Where(i => i.Roles.Any(j => j.RoleId == role.Id)).ToList();

            return result;
        }

        /*public static ApplicationUser CreateUser(string Email, string roleName)
        {
            var user = new ApplicationUser() { UserName = Email, Email = Email };
            userManager.AddToRole(user.Id, roleName);

            return user;
        }

        public static void DeleteUser(string userId)
        {
            var user = userManager.FindById(userId);
            var currentUserRole = Roles.GetRolesForUser(user.UserName).ToString();

            userManager.RemoveFromRole(userId, currentUserRole);
            userManager.Delete(user);

        }*/
    }
}

