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
using App.Web.Models;
using App.Web.Areas.AccessManagement.Models;
using App.Entities;

namespace App.Web.Areas.AccessManagement.Controllers
{
    [ControllerAuthorize]
    public class UserController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: AccessManagement/User
        public ActionResult Index(UserSearchFormVM model = null)
        {
            if (model == null)
            {
                model = new UserSearchFormVM();
                //model.IsActive = true;
            }
            
            return View(model);
        }

        [HttpGet]
        public ActionResult AjaxUser(JQueryDataTableParamModel param)
        {
            var QS = Request.QueryString;
            String Username = QS["Username"];
            //Boolean IsActive = (QS["IsActive"] == "false" ? false : true);

            List<string[]> listResult = new List<string[]>();
            String errorMessage = "";
            
            try
            {
                IEnumerable<User> Query = db.Users;
                if (Username != "")
                {
                    Query = Query.Where(x => x.Username.Contains(Username));
                }

                //Query = Query.Where(x => x.IsActive == IsActive);

                int TotalRecord = Query.Count();

                var OrderedQuery = Query.OrderBy(x => x.UserId);

                int pageSize = param.iDisplayLength;
                int pageNumber = param.iDisplayStart == 0 ? 1 : (param.iDisplayStart / param.iDisplayLength) + 1; ;
                var PagedQuery = OrderedQuery.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();

                int i = 0;
                foreach (var data in PagedQuery)
                {
                    i++;
                    listResult.Add(new string[]
                    {
                        i.ToString(),
                        data.Username,
                        //(data.IsActive == true ? "<input type=\"checkbox\" disabled checked>" : "<input type=\"checkbox\" disabled>"),
                        data.UserId.ToString()
                    });
                }
                return Json(new
                {
                    sEcho = param.sEcho,
                    iTotalRecords = TotalRecord,
                    iTotalDisplayRecords = TotalRecord,
                    aaData = listResult
                },
                JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }

            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = 0,
                iTotalDisplayRecords = 0,
                aaData = listResult,
                error = errorMessage
            },
            JsonRequestBehavior.AllowGet);
        }

        // GET: AccessManagement/User/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        public ActionResult ManageRole(int? UserId)
        {
            if (UserId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(UserId);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManageRole(AddRoleToUserFormVM model)
        {
            var User = db.Users.Find(model.UserId);
            if (User == null)
            {
                return HttpNotFound();
            }
            if (ModelState.IsValid)
            {
                User.Roles.Add(db.Roles.Find(model.RoleId));

                db.Entry(User).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ManageRole", new { UserId = model.UserId});
            }
            return View(User);
        }

        // GET: AccessManagement/User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AccessManagement/User/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,Fullname, Username,Password")] User user)
        {
            if (ModelState.IsValid)
            {
                user.Password = Security.GetHashString(user.Password);
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        public ActionResult RemoveRoleFromUser(int UserId, int RoleId)
        {
            DatabaseContext db = new DatabaseContext();
            RemoveRoleFromUserFormVM model = new RemoveRoleFromUserFormVM();
            model.UserId = UserId;
            model.RoleId = RoleId;
            model.User = db.Users.Find(UserId);
            model.Role = db.Roles.Find(RoleId);

            return View(model);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveRoleFromUser(RemoveRoleFromUserFormVM model)
        {
            if (ModelState.IsValid)
            {
                var User = db.Users.Find(model.UserId);
                var Role = db.Roles.Find(model.RoleId);
                User.Roles.Remove(Role);

                db.Entry(User).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("ManageRole",new { UserId = model.UserId});
            }

            return View(model);
        }

        // GET: AccessManagement/User/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: AccessManagement/User/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,Fullname, Username,Password")] User user)
        {
            if (ModelState.IsValid)
            {
                User UserCek = db.Users.Find(user.UserId);
                UserCek.Username = user.Username;
                //UserCek.IsActive = user.IsActive;
                if (user.Password != UserCek.Password)
                {
                    UserCek.Password = Security.GetHashString(user.Password);
                }
                db.Entry(UserCek).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: AccessManagement/User/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: AccessManagement/User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
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
