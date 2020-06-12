using Electronicsphone.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using System;

[assembly: OwinStartupAttribute(typeof(Electronicsphone.Startup))]
namespace Electronicsphone
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRolesandUser();
        }

        private void createRolesandUser()
        {
			ApplicationDbContext Context = new ApplicationDbContext();
			var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(Context));
			var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(Context));
			if (!roleManager.RoleExists("Admin"))
			{
				//tạo ra admin role đầu tiên
				var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
				role.Name = "Admin";
				roleManager.Create(role);

				var user = new ApplicationUser();
				user.UserName = "son";
				user.Email = "ledinhson2016@gmail.com";
				string userPWD = "Ledinhson1998@";

				var chooseUser = UserManager.Create(user, userPWD);

				if (chooseUser.Succeeded)
				{
					var result1 = UserManager.AddToRole(user.Id, "Admin");
				}
			}
            // creating Creating Manager role     
            if (!roleManager.RoleExists("Manager"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Manager";
                roleManager.Create(role);

            }

            // creating Creating Employee role     
            if (!roleManager.RoleExists("Employee"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Employee";
                roleManager.Create(role);

            }
        }
    }
}
