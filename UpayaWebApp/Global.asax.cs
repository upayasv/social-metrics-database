using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using UpayaWebApp.Models;

namespace UpayaWebApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            try
            {
                InitRoles();
            }
            catch (Exception ex)
            {

            }
        }

        void InitRoles()
        {
            var context = new ApplicationDbContext();
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            // Create Admin Role
            string[] roles = { Constants.SYS_ADMIN, Constants.UPAYA_ADMIN, Constants.PARTNER_ADMIN, Constants.STAFF_MEMBER };
            IdentityResult roleResult;

            // Check to see if Role Exists, if not create it
            foreach (string roleName in roles)
                if (!RoleManager.RoleExists(roleName))
                {
                    roleResult = RoleManager.Create(new IdentityRole(roleName));
                }

            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var user = new ApplicationUser() { UserName = "sysadmin" };
            var result = UserManager.Create(user, "654321");
            if (result.Succeeded)
            {
                UserManager.AddToRole(user.Id, Constants.SYS_ADMIN);
            }
        }
    }
}
