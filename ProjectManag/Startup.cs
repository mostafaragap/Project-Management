using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using ProjectManag.Models;

[assembly: OwinStartupAttribute(typeof(ProjectManag.Startup))]
namespace ProjectManag
{
    public partial class Startup
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            defaultRoleAndUsers();
        }
        public void defaultRoleAndUsers()
        {
            var rolemanager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var usermanager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            IdentityRole role = new IdentityRole();
            if (!rolemanager.RoleExists("Admin"))
            {
                role.Name = "Admin";
                rolemanager.Create(role);
                ApplicationUser user = new ApplicationUser();
                user.UserName = "Admin";
                user.Email = "admin@yahoo.com";
                user.UserType = "Admin";
                var check = usermanager.Create(user, "MOstafa350_");
                if (check.Succeeded)
                {
                    usermanager.AddToRole(user.Id, "Admin");
                }
            }

        }
    }
}
