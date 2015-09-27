using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Project.Domain.Entities;
using Project.Domain.Enums;

namespace Project.Data
{
    public class ProjectSeedData : DropCreateDatabaseIfModelChanges<ProjectContext>
    {
        protected override void Seed(ProjectContext context)
        {
            AddAdminUser(context);

            context.Commit();
        }

        private static string AddAdminUser(ProjectContext context)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            var roleManager = new RoleManager<IdentityRole>(new
                                       RoleStore<IdentityRole>(context));

            const string username = "admin@yoursite.com";
            const string password = "password!";

            if (!roleManager.RoleExists(UserType.Admin.ToString()))
                roleManager.Create(new IdentityRole(UserType.Admin.ToString()));

            var user = new ApplicationUser { UserName = username, EmailConfirmed = true, Email = username};
            var adminresult = userManager.Create(user, password);

            if (adminresult.Succeeded)
                userManager.AddToRole(user.Id, UserType.Admin.ToString());

            context.Commit();

            return user.Id;
        }
    }
}
