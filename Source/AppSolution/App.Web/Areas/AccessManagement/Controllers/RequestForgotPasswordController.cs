using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using App.Entities.DataAccessLayer;
using App.Entities.Models;
using App.Web.Areas.AccessManagement.Models;
using App.Web.Common;
using App.Entities;

namespace App.Web.Areas.AccessManagement.Controllers
{
    public class RequestForgotPasswordController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: AccessManagement/RequestForgotPassword
        public ActionResult Index()
        {
            return View(db.RequestForgotPasswords.OrderBy(x => x.RequestDate).ToList());
        }


        public ActionResult UpdateUserPassword(String Email)
        {
            UpdateUserPasswordFormVM model = new UpdateUserPasswordFormVM();
            model.Email = Email;

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateUserPassword(UpdateUserPasswordFormVM model)
        {
            if (ModelState.IsValid)
            {
                User User = db.Users.Where(x => x.Email == model.Email).FirstOrDefault();
                User.Password = Security.GetHashString(model.NewPassword);
                db.Entry(User).State = EntityState.Modified;
                db.SaveChanges();                

                var UserModules = Function.UserModules();
                UserModules = UserModules.Where(x => 
                    x.Area.ToLower() == "AccessManagement".ToLower()
                    && x.Controller.ToLower() == "RequestForgotPassword".ToLower()
                    && x.Action.ToLower() == "Index".ToLower()).ToList();

                if (UserModules.Count() > 0)
                {
                    RequestForgotPassword requestForgotPassword = db.RequestForgotPasswords.Where(x => x.Email == model.Email).FirstOrDefault();
                    if (requestForgotPassword != null)
                    {
                        db.RequestForgotPasswords.Remove(requestForgotPassword);
                        db.SaveChanges();
                    }

                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.MessageInfo = "Update Password Success";

                    return View(model);
                }
            }
            return View(model);
        }        

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
