using App.Entities.DataAccessLayer;
using App.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace App.Web
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class ControllerAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(System.Web.HttpContextBase httpContext)
        {

            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var rd = httpContext.Request.RequestContext.RouteData;
                var dt = rd.DataTokens;
                string currentAction = rd.GetRequiredString("action");
                string currentController = rd.GetRequiredString("controller");
                string currentArea = dt["area"] as string;

                List<String> AllowedAcction = new List<string>();
                AllowedAcction.Add("ajax");
                AllowedAcction.Add("UpdateUserPassword".ToLower());
                AllowedAcction.Add("logoff");

                if (AllowedAcction.Where(x => currentAction.ToLower().Contains(x.ToLower())).Count() > 0)
                {
                    return true;
                }
                DatabaseContext db = new DatabaseContext();
                Module Module = db.Modules.Where(
                    x => 
                        x.Action == currentAction 
                        && x.Controller == currentController
                        && x.Area == currentArea).FirstOrDefault();

                if (Module == null)
                {
                    return false;
                }

                List<Role> Roles = new List<Role>();
                foreach (var item in Module.Roles)
                {
                    IEnumerable<Role> Query = db.Roles;
                    var CurrentQuery = Query.Where(x => x.RoleId == item.RoleId).ToList();
                    var QueryChild = new List<Role>();
                    Boolean IsFirst = true;
                    while (CurrentQuery != null && CurrentQuery.Count() > 0)
                    {
                        if (!IsFirst)
                        {
                            QueryChild.AddRange(CurrentQuery);
                        }
                        else
                        {
                            IsFirst = false;
                        }
                        CurrentQuery = CurrentQuery.SelectMany(x => x.Roles).ToList();
                    }

                    Roles.AddRange(QueryChild);
                }
                Roles.AddRange(Module.Roles);
                Roles = Roles.Where(x => x.IsActive == true).ToList();

                User User = db.Users.Where(
                    x => 
                        x.Email == HttpContext.Current.User.Identity.Name 
                        && x.IsActive == true
                ).FirstOrDefault();

                if (User == null)
                {
                    return false;
                }
                List<Role> UserRoles = new List<Role>();
                foreach (var item in User.Roles)
                {
                    IEnumerable<Role> Query = db.Roles;
                    var CurrentQuery = Query.Where(x => x.RoleId == item.RoleId).ToList();
                    var QueryChild = new List<Role>();
                    Boolean IsFirst = true;
                    while (CurrentQuery != null && CurrentQuery.Count() > 0)
                    {
                        if (!IsFirst)
                        {
                            QueryChild.AddRange(CurrentQuery);
                        }
                        else
                        {
                            IsFirst = false;
                        }
                        CurrentQuery = CurrentQuery.SelectMany(x => x.Roles).ToList();
                    }

                    UserRoles.AddRange(QueryChild);
                }
                UserRoles.AddRange(User.Roles);
                UserRoles = UserRoles.Where(x => x.IsActive == true).ToList();

                var RoleAuthorized = Roles.Where(x => UserRoles.Contains(x));
                if (RoleAuthorized.Count() > 0 )
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            base.HandleUnauthorizedRequest(filterContext);
            
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    action = "Index",
                    controller = "Unauthorized",
                    area = "",
                    url = filterContext.HttpContext.Request.Url
                }));
            }
            else
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    action = "Login",
                    controller = "Account",
                    area = "",
                    returnUrl = filterContext.HttpContext.Request.Url
                }));
            }            
        }
    }
}