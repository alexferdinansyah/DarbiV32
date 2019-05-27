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
    public class TingkatController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: MasterData/Tingkat
        public ActionResult Index(TingkatSearchFormVM model = null)
        {
            if (model == null)
            {
                model = new TingkatSearchFormVM();
                //model.IsActive = true;
            }

            return View(model);
        }

        //GET : MasterData/Tingkat/Create
        public ActionResult Create()
        {
            return View();
        }

        //POST : MasterData/Tingkat/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Tingkat tingkat)
        {
            if (ModelState.IsValid)
            {
                db.Tingkats.Add(tingkat);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tingkat);
        }

        //Tampil Data Table
        [HttpGet]
        public ActionResult AjaxTingkat(JQueryDataTableParamModel param)
        {
            var QS = Request.QueryString;
            String Namatingkat = QS["Namatingkat"];
            //String Jenjang = QS["Jenjang"];
            //Boolean IsActive = (QS["IsActive"] == "false" ? false : true);

            List<string[]> listResult = new List<string[]>();
            String errorMessage = "";

            try
            {
                IEnumerable<Tingkat> Query = db.Tingkats;
                if (Namatingkat != "")
                {
                    Query = Query.Where(x => x.Namatingkat.Contains(Namatingkat));
                }
                if (Jenjang != null)
                {
                    Query = Query.Where(x => x.Jenjang.Contains(Jenjang));
                }

                //Query = Query.Where(x => x.IsActive == IsActive);

                int TotalRecord = Query.Count();

                var OrderedQuery = Query.OrderBy(x => x.TingkatId);

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
                        data.Namatingkat,
                        data.Jenjang,
                        //(data.IsActive == true ? "<input type=\"checkbox\" disabled checked>" : "<input type=\"checkbox\" disabled>"),
                        data.TingkatId.ToString()
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

        // GET: MasterData/Tingkat/Delete
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tingkat tingkat = db.Tingkats.Find(id);
            if (tingkat == null)
            {
                return HttpNotFound();
            }
            return View(tingkat);
        }

        // POST: MasterData/Tingkat/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tingkat tingkat = db.Tingkats.Find(id);
            db.Tingkats.Remove(tingkat);
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
