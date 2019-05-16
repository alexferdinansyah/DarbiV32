using App.Entities.DataAccessLayer;
using App.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace App.Web.Common
{
    public class Function
    {
        public static List<Module> UserModules()
        {
            DatabaseContext db = new DatabaseContext();

            List<Module> UserModules = new List<Module>();

            User User = db.Users.Where(
                    x =>
                        x.Email == HttpContext.Current.User.Identity.Name
                        && x.IsActive == true
                ).FirstOrDefault();

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

            foreach (var item in UserRoles)
            {
                UserModules.AddRange(item.Modules);
            }

            UserModules = UserModules.Where(x => x.IsActive == true && x.Action.ToLower().Contains("index")).ToList();

            return UserModules;
        }

        public static List<String> GroupUserModules()
        {

            List<Module> UserModules = Function.UserModules();
            List<String> UserArea = new List<string>();

            UserArea = UserModules.Select(x => x.Area).Distinct().OrderBy(x => x).ToList();

            return UserArea;
        }

        public static List<Module> UserModulesByArea(String Area)
        {
            List<Module> UserModules = Function.UserModules();

            UserModules = UserModules.Where(x => x.Area == Area).OrderBy(x => x.Name).ToList();

            return UserModules;
        }
        public static string CamelCaseToSentenceCase(String CamelCase)
        {
            return Regex.Replace(CamelCase, "(\\B[A-Z])", " $1");
        }

        public static string CurrentAreaName()
        {
            HttpContext context = HttpContext.Current;
            var rd = context.Request.RequestContext.RouteData;
            var dt = rd.DataTokens;

            string currentArea = dt["area"] as string;

            return currentArea;
        }
    }
}