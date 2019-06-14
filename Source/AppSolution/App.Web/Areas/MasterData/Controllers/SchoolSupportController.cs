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
using App.Web.Areas.MasterData.Models;
using App.Entities;

namespace App.Web.Areas.MasterData.Controllers
{
    [ControllerAuthorize]
    public class SchoolSupportController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: MasterData/SchoolSupport
        public ActionResult Index(SchoollSupportSearchFormVM model = null)
        {
            if (model == null)
            {
                model = new SchoollSupportSearchFormVM();
            }

            return View(model);
        }


        [HttpGet]
        public ActionResult AjaxSS(JQueryDataTableParamModel param)
        {
            var QS = Request.QueryString;
            String JenisSS = QS["JenisSS"];
            //Boolean IsActive = (QS["IsActive"] == "false" ? false : true);

            List<string[]> listResult = new List<string[]>();
            String errorMessage = "";

            try
            {
                IEnumerable<SchoolSupport> Query = db.SchoolSupports;
                if (JenisSS != "")
                {
                    Query = Query.Where(x => x.JenisSS.Contains(JenisSS));
                }

                //Query = Query.Where(x => x.IsActive == IsActive);

                int TotalRecord = Query.Count();

                var OrderedQuery = Query.OrderBy(x => x.SsId);

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
                        data.JenisSS,
                        //(data.IsActive == true ? "<input type=\"checkbox\" disabled checked>" : "<input type=\"checkbox\" disabled>"),
                        data.SsId.ToString()
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

        //GET : MasterData/SchoolSupport/Create
        public ActionResult Create()
        {
            return View();
        }

        //POST : MasterData/SchoolSupport/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SchoolSupport schoolsupport)
        {
            if (ModelState.IsValid)
            {
                db.SchoolSupports.Add(schoolsupport);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(schoolsupport);
        }

        // GET: AccessManagement/User/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SchoolSupport schoolsupport = db.SchoolSupports.Find(id);
            if (schoolsupport == null)
            {
                return HttpNotFound();
            }
            return View(schoolsupport);
        }

        // POST: MasterData/SchoolSupport/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SsId,JenisSS")] SchoolSupport schoolsupport)
        {
            if (ModelState.IsValid)
            {
                SchoolSupport SScek = db.SchoolSupports.Find(schoolsupport.SsId);
                SScek.JenisSS = schoolsupport.JenisSS;
               
                db.Entry(SScek).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(schoolsupport);
        }

        // GET: MasterData/SchoolSUpport/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SchoolSupport schoolsupport = db.SchoolSupports.Find(id);
            if (schoolsupport == null)
            {
                return HttpNotFound();
            }
            return View(schoolsupport);
        }

        // POST: MasterData/SchoolSupport/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SchoolSupport schoolsupport = db.SchoolSupports.Find(id);
            db.SchoolSupports.Remove(schoolsupport);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: MasterData/SchoolSupport/Details/5
        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SchoolSupport schoolsupport = db.SchoolSupports.Find(id);
            if (schoolsupport == null)
            {
                return HttpNotFound();
            }
            return View(schoolsupport);
        }
    }
}