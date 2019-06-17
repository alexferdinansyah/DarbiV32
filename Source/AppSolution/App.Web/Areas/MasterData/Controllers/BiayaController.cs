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
    public class BiayaController : Controller

    {
        private DatabaseContext db = new DatabaseContext();
        // GET: MasterData/Biaya
        public ActionResult Index(BiayaSearchFormVM model = null)
        {
            if (model == null)
            {
                model = new BiayaSearchFormVM();
            }

            return View(model);
        }

        //GET : MasterData/Biaya/Create
        public ActionResult Create()
        {
            CreateBiaya model = new CreateBiaya();

            List<SelectListItem> ObjItem = new List<SelectListItem>()

            {
                new SelectListItem {Text="Pilih Kategori",Value="0",Selected=true },
                new SelectListItem {Text="Biaya Masuk",Value="1" },
                new SelectListItem {Text="SPP",Value="2"},
                new SelectListItem {Text="School Support",Value="3"},
            };

            List<SelectListItem> ObjJenis = new List<SelectListItem>()

            {
                //new SelectListItem {Text="Pilih Kategori",Value="0",Selected=true },                
            };

            ViewBag.JenisItem = ObjJenis;
            ViewBag.ListItem = ObjItem;
            return View(model);
        }

        //POST : Masterdata/Biaya/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateBiaya model)
        {
            if (ModelState.IsValid)
            {
                Biaya newmodel = new Biaya();
                newmodel.KatBiaya = model.KatBiaya;
                newmodel.JenisBiaya = model.JenisBiaya;
                newmodel.TingkatId = model.TingkatId;
                newmodel.NomBiaya = model.NomBiaya;

                //db.Biayas.Add(newmodel);
                //db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        //[HttpGet]
        public ActionResult AjaxBiaya(JQueryDataTableParamModel param)
        {
            var QS = Request.QueryString;
            String KatBiaya = QS["KatBiaya"];
            //Boolean IsActive = (QS["IsActive"] == "false" ? false : true);

            List<string[]> listResult = new List<string[]>();
            String errorMessage = "";

            try
            {
                IEnumerable<Biaya> Query = db.Biayas;
                if (KatBiaya != "")
                {
                    Query = Query.Where(x => x.KatBiaya.Contains(KatBiaya));
                }
                //if (Jenjang != null)
                //{
                //    Query = Query.Where(x => x.Jenjang.Contains(Jenjang));
                //}


                int TotalRecord = Query.Count();

                var OrderedQuery = Query.OrderBy(x => x.BiayaId);

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
                        data.KatBiaya,
                        //data.JenisBiaya,
                        //data.Tingkats.TingkatId,
                        //data.NomBiaya,
                        //data.Jenjang,
                        //(data.IsActive == true ? "<input type=\"checkbox\" disabled checked>" : "<input type=\"checkbox\" disabled>"),
                        data.BiayaId.ToString()
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