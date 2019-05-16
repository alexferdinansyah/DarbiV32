using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using App.Entities.DataAccessLayer;
using System.Web.Helpers;
using System.Security.Claims;

namespace App.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            DatabaseContext con = new DatabaseContext();
            con.Database.Initialize(true);

            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.Name;

            GlobalFilters.Filters.Add(new ControllerAuthorizeAttribute());
        }
    }
}
