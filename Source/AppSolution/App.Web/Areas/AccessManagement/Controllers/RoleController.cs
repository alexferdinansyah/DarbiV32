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

namespace App.Web.Areas.AccessManagement.Controllers
{
    public class RoleController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: AccessManagement/Role
        public ActionResult Index(RoleSearchFormVM model = null)
        {
            if (model == null)
            {
                model = new RoleSearchFormVM();
                model.IsActive = true;
            }

            return View(model);
        }


        [HttpGet]
        public ActionResult AjaxRole(JQueryDataTableParamModel param)
        {
            var QS = Request.QueryString;
            String Name = QS["Name"];
            String Parent = QS["RoleId"];
            Boolean IsActive = (QS["IsActive"] == "false" ? false : true);

            List<string[]> listResult = new List<string[]>();
            String errorMessage = "";

            try
            {
                IEnumerable<Role> Query = db.Roles;
                if (Parent != "" && Parent != null)
                {
                    var CurrentQuery = Query.Where(x => x.RoleId == int.Parse(Parent)).ToList();
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

                    Query = QueryChild;
                }
                Query = Query.Where(x => x.IsActive == IsActive);
                if (Name != "")
                {
                    Query = Query.Where(x => x.Name.Contains(Name));
                }

                int TotalRecord = Query.Count();

                var OrderedQuery = Query.OrderBy(x => x.Name);

                int pageSize = param.iDisplayLength;
                int pageNumber = param.iDisplayStart == 0 ? 1 : (param.iDisplayStart / param.iDisplayLength) + 1; ;
                var PagedQuery = OrderedQuery.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();

                int i = 0;
                foreach (var data in PagedQuery)
                {
                    var RoleChild = db.Roles.Where(x => x.RoleId == data.RoleId).FirstOrDefault();
                    String ParentName = data.Name;
                    if (RoleChild != null)
                    {
                        var ParentRole = db.Roles.Where(x => x.Roles.Any(z => z.RoleId == RoleChild.RoleId)).FirstOrDefault();
                        while (ParentRole != null)
                        {
                            ParentName += " &#8594; " + ParentRole.Name;
                            ParentRole = db.Roles.Where(x => x.Roles.Any(z => z.RoleId == ParentRole.RoleId)).FirstOrDefault();
                        }
                    }

                    i++;
                    listResult.Add(new string[]
                    {
                        i.ToString(),
                        data.Name,
                        ParentName,
                        (data.IsActive == true ? "<input type=\"checkbox\" disabled checked>" : "<input type=\"checkbox\" disabled>"),
                        data.RoleId.ToString()
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

        public ActionResult AjaxAllRoleChild(JQueryDataTableParamModel param)
        {
            var QS = Request.QueryString;
            String RoleId = QS["RoleId"];

            List<string[]> listResult = new List<string[]>();
            String errorMessage = "";

            try
            {
                Role Role = db.Roles.Find(int.Parse(RoleId));
                List<Role> Query = Role.Roles.SelectMany(x => x.Roles).ToList();
                
                Boolean start = true;
                var AllChild = Query;
                do
                {
                    var CurrentChild = AllChild.SelectMany(x => x.Roles).ToList();
                    if (CurrentChild.Count() <= 0)
                    {
                        start = false;
                    }
                    else
                    {
                        AllChild = CurrentChild;
                        Query.AddRange(CurrentChild);
                    }

                } while (start);

                Query.AddRange(Role.Roles);
                Query.Add(Role);

                int TotalRecord = Query.Count();

                var OrderedQuery = Query.OrderBy(x => x.Name);

                int pageSize = param.iDisplayLength;
                int pageNumber = param.iDisplayStart == 0 ? 1 : (param.iDisplayStart / param.iDisplayLength) + 1; ;
                var PagedQuery = OrderedQuery.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();

                int i = 0;
                foreach (var data in PagedQuery)
                {
                    var RoleChild = db.Roles.Where(x => x.RoleId == data.RoleId).FirstOrDefault();
                    String ParentName = data.Name;
                    if (RoleChild != null)
                    {
                        var ParentRole = db.Roles.Where(x => x.Roles.Any(z => z.RoleId == RoleChild.RoleId)).FirstOrDefault();
                        while (ParentRole != null)
                        {
                            ParentName += " &#8594; " + ParentRole.Name;
                            ParentRole = db.Roles.Where(x => x.Roles.Any(z => z.RoleId == ParentRole.RoleId)).FirstOrDefault();
                        }
                    }
                    String ListModule = "";
                    foreach (var modul in data.Modules)
                    {
                        ListModule += (modul.IsActive ? "<input type=\"checkbox\" disabled checked>" : "<input type=\"checkbox\" disabled>") + " " + modul.Name + "<br />";
                    }
                    i++;
                    listResult.Add(new string[]
                    {
                        i.ToString(),
                        data.Name,
                        ParentName,
                        ListModule
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

        public ActionResult ManageModule(int? RoleId)
        {
            if (RoleId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role role = db.Roles.Find(RoleId);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManageModule(AddModuleToRoleFormVM model)
        {
            Role role = db.Roles.Find(model.RoleId);
            if (ModelState.IsValid)
            {
                Module Module = db.Modules.Find(model.ModuleId);
                role.Modules.Add(Module);

                db.Entry(role).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("ManageModule", new { RoleId = model.RoleId });
            }
            return View(role);
        }

        public ActionResult RemoveModuleFromRole(int ModuleId, int RoleId)
        {
            DatabaseContext db = new DatabaseContext();
            RemoveModuleFromRoleFormVM model = new RemoveModuleFromRoleFormVM();
            model.ModuleId = ModuleId;
            model.RoleId = RoleId;
            model.Module = db.Modules.Find(ModuleId);
            model.Role = db.Roles.Find(RoleId);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveModuleFromRole(RemoveModuleFromRoleFormVM model)
        {
            if (ModelState.IsValid)
            {
                var Module = db.Modules.Find(model.ModuleId);
                var Role = db.Roles.Find(model.RoleId);
                Role.Modules.Remove(Module);

                db.Entry(Role).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("ManageModule", new { RoleId = model.RoleId });
            }

            return View(model);
        }

        public ActionResult ManageChild(int? RoleId)
        {
            if (RoleId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role role = db.Roles.Find(RoleId);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManageChild(AddChildToRoleFormVM model)
        {
            Role role = db.Roles.Find(model.RoleId);
            if (ModelState.IsValid)
            {
                Role roleChild = db.Roles.Find(model.RoleIdChild);
                role.Roles.Add(roleChild);

                db.Entry(role).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("ManageChild", new { RoleId = model.RoleId});
            }
            return View(role);
        }
        // GET: AccessManagement/Role/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role role = db.Roles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }


        public ActionResult RemoveChildFromRole(int RoleIdChild, int RoleId)
        {
            DatabaseContext db = new DatabaseContext();
            RemoveChildFromRoleFormVM model = new RemoveChildFromRoleFormVM();
            model.RoleIdChild = RoleIdChild;
            model.RoleId = RoleId;
            model.RoleChild = db.Roles.Find(RoleIdChild);
            model.Role = db.Roles.Find(RoleId);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveChildFromRole(RemoveChildFromRoleFormVM model)
        {
            if (ModelState.IsValid)
            {
                var RoleChild = db.Roles.Find(model.RoleIdChild);
                var Role = db.Roles.Find(model.RoleId);
                Role.Roles.Remove(RoleChild);

                db.Entry(Role).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("ManageChild", new { RoleId = model.RoleId });
            }

            return View(model);
        }

        // GET: AccessManagement/Role/Create
        public ActionResult Create()
        {
            CreateRoleFormVM model = new CreateRoleFormVM();

            return View(model);
        }

        // POST: AccessManagement/Role/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateRoleFormVM model)
        {
            if (ModelState.IsValid)
            {

                Role role = new Role();
                role.Name = model.Name;
                role.IsActive = model.IsActive;

                Role ParentRole = db.Roles.Find(model.RoleIdParent);

                if (ParentRole != null)
                {
                    ParentRole.Roles.Add(role);

                    db.Entry(ParentRole).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    db.Roles.Add(role);
                    db.SaveChanges();
                }
                
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: AccessManagement/Role/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }            
            Role role = db.Roles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            EditRoleFormVM model = new EditRoleFormVM();
            model.RoleId = role.RoleId;
            model.Name = role.Name;
            model.IsActive = role.IsActive;
            Role ParentRole = db.Roles.Where(x => x.Roles.Any(z => z.RoleId == role.RoleId)).FirstOrDefault();
            if (ParentRole != null)
            {
                model.RoleIdParentNew = ParentRole.RoleId;
                model.RoleIdParentOld = ParentRole.RoleId;
            }
            return View(model);
        }

        // POST: AccessManagement/Role/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditRoleFormVM model)
        {
            var role = db.Roles.Find(model.RoleId);
            if (role == null)
            {
                return HttpNotFound();
            }
            role.Name = model.Name;
            role.IsActive = model.IsActive;
            if (ModelState.IsValid)
            {
                db.Entry(role).State = EntityState.Modified;
                db.SaveChanges();
                if (model.RoleIdParentNew != model.RoleIdParentOld)
                {
                    if (model.RoleIdParentOld != null && model.RoleIdParentNew != null)
                    {
                        var RoleParentOld = db.Roles.Find(model.RoleIdParentOld);
                        RoleParentOld.Roles.Remove(role);
                        db.Entry(RoleParentOld).State = EntityState.Modified;
                        db.SaveChanges();

                        var RoleParentNew = db.Roles.Find(model.RoleIdParentNew);
                        RoleParentNew.Roles.Add(role);
                        db.Entry(RoleParentNew).State = EntityState.Modified;
                        db.SaveChanges();
                    }


                    if (model.RoleIdParentOld == null && model.RoleIdParentNew != null)
                    {
                        var RoleParentNew = db.Roles.Find(model.RoleIdParentNew);
                        RoleParentNew.Roles.Add(role);
                        db.Entry(RoleParentNew).State = EntityState.Modified;
                        db.SaveChanges();
                    }

                    if (model.RoleIdParentOld != null && model.RoleIdParentNew == null)
                    {
                        var RoleParentOld = db.Roles.Find(model.RoleIdParentOld);
                        RoleParentOld.Roles.Remove(role);
                        db.Entry(RoleParentOld).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                
                return RedirectToAction("Index");
            }
            return View(role);
        }

        // GET: AccessManagement/Role/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role role = db.Roles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // POST: AccessManagement/Role/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Role role = db.Roles.Find(id);
            db.Roles.Remove(role);
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
