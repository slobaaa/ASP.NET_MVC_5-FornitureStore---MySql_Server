using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using ProdavnicaNamestaja.Models;
using Owin;
using System.Security.Claims;

[assembly: OwinStartupAttribute(typeof(ProdavnicaNamestaja.Startup))]
namespace ProdavnicaNamestaja
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
			createRolesandUsers();
		}


		// In this method we will create default User roles and Admin user for login
		private void createRolesandUsers()
		{
			ApplicationDbContext context = new ApplicationDbContext();

			var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
			var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


			// In Startup iam creating first Admin Role and creating a default Admin User 
			if (!roleManager.RoleExists("Admin"))
			{

				// first we create Admin rool
				var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
				role.Name = "Admin";
				roleManager.Create(role);

				//Here we create a Admin super user who will maintain the website				

				var user = new ApplicationUser();
                //user.UserName = "shanu";
                user.UserName = "admin";
                //user.Email = "syedshanumcain@gmail.com";
                user.Email = "svujcin@yahoo.com";

                //string userPWD = "A@Z200711";
                string userPWD = "Sloba123456-";

                var chkUser = UserManager.Create(user, userPWD);

				//Add default User to Role Admin
				if (chkUser.Succeeded)
				{
					var result1 = UserManager.AddToRole(user.Id, "Admin");

				}
			}

			// creating Creating Manager role 
			//if (!roleManager.RoleExists("Manager"))
			//{
			//	var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
			//	role.Name = "Manager";
			//	roleManager.Create(role);

			//}
            if (!roleManager.RoleExists("User"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "User";
                roleManager.Create(role);
            }
            //if (!roleManager.RoleExists("admin"))
            //{
            //    var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
            //    role.Name = "Admin";
            //    roleManager.Create(role);
            //}

            // creating Creating Employee role 
            //if (!roleManager.RoleExists("Employee"))
            //{
            //	var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
            //	role.Name = "Employee";
            //	roleManager.Create(role);

            //}
        }
	}
}
