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
using App.Web.Areas.Recapitulation.Models;
using App.Entities;

namespace App.Web.Areas.Recapitulation.Controllers
{

    public class RekapBiayaMasukController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: Recapitulation/RekapBiayaMasuk
        public ActionResult Index(SearchRekapBiayaMasuk model = null)
        {
            if (model == null)
            {
                model = new SearchRekapBiayaMasuk();
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult AjaxRekapBiayaMasuk(JQueryDataTableParamModel param, SearchRekapBiayaMasuk m)
        {
            var QS = Request.QueryString;
            string Namasiswa = m.Namasiswa;
            //Boolean IsActive = (QS["IsActive"] == "false" ? false : true);

            List<RekapBiayaMasukVM> models = new List<RekapBiayaMasukVM>();
            RekapBiayaMasukVM model = new RekapBiayaMasukVM();
            List<string[]> listResult = new List<string[]>();
            String errorMessage = "";
            if (Namasiswa == "")
            {
                return Json(new
                {
                    sEcho = param.sEcho,
                    iTotalRecords = 0,
                    iTotalDisplayRecords = 0,
                    aaData = models,
                    error = errorMessage
                },
            JsonRequestBehavior.AllowGet);
            }
            try
            {
                IEnumerable<Siswa> datasiswa = db.Siswas.Where(x => x.Fullname.ToLower().Contains(Namasiswa.ToLower()));
                string Nosisda = "";
                foreach (var d in datasiswa)
                {
                    model.Nosisda = d.Nosisda;
                    model.Namasiswa = d.Fullname;
                }
                models.Add(model);
                int TotalRecord = models.Count();

                int pageSize = param.iDisplayLength;
                int pageNumber = param.iDisplayStart == 0 ? 1 : (param.iDisplayStart / param.iDisplayLength) + 1; ;
                var PagedQuery = models.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();

                int i = 0;
                foreach (var data in PagedQuery)
                {
                    i++;
                    listResult.Add(new string[]
                    {
                        i.ToString(),
                        data.Nosisda,
                        data.Namasiswa,
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