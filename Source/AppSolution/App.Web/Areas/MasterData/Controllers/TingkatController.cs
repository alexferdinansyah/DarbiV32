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

            CreateTingkatFormVM model = new CreateTingkatFormVM();

            return View(model);
        }

        //POST : MasterData/Tingkat/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateTingkatFormVM model)
        {
            if (ModelState.IsValid)
            {
                Tingkat newmodel = new Tingkat();
                newmodel.Namatingkat = model.Namatingkat;
                newmodel.JenjangId = model.JenjangId;

                db.Tingkats.Add(newmodel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        //Tampil Data Table
        [HttpGet]
        public ActionResult AjaxTingkat(JQueryDataTableParamModel param)
        {
            var QS = Request.QueryString;
            String Namatingkat = QS["Namatingkat"];
            String Jenjang = QS["NamaJenjang"];
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
                //if (Jenjang != null)
                //{
                //    Query = Query.Where(x => x.Jenjang.Contains(Jenjang));
                //}


                int TotalRecord = Query.Count();

                var OrderedQuery = Query.OrderBy(x => x.TingkatId);

                int pageSize = param.iDisplayLength;
                int pageNumber = param.iDisplayStart == 0 ? 1 : (param.iDisplayStart / param.iDisplayLength) + 1;
                var PagedQuery = OrderedQuery.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();

                int i = 0;
                foreach (var data in PagedQuery)
                {
                    i++;
                    listResult.Add(new string[]
                    {
                        i.ToString(),
                        data.Namatingkat,
                        data.Jenjangs.JenjangName,
                        //data.Jenjang,
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



    }
}
