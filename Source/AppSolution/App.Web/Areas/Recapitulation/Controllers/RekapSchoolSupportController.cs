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

    public class RekapSchoolSupportController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: Recapitulation/RekapSchoolSupport
        public ActionResult Index(SearchRekapBiayaMasuk model = null)

        {
            List<SelectListItem> OpBM = new List<SelectListItem>()

            {
                new SelectListItem {Text="--- Pilih ---",Value="0",Selected=true},
                new SelectListItem {Text="Nama",Value="1"},
                new SelectListItem {Text="Tanggal",Value="2"},
            };

            ViewBag.OpBM = OpBM;
            return View(model);
        }

        [HttpGet]
        public ActionResult AjaxRekapSchoolSupport(JQueryDataTableParamModel param, SearchRekapBiayaMasuk m)
        {
            var QS = Request.QueryString;
            string Namasiswa = m.Namasiswa;

            List<RekapSchoolSupportVM> models = new List<RekapSchoolSupportVM>();
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
                    RekapSchoolSupportVM model = new RekapSchoolSupportVM();
                    model.Nosisda = d.Nosisda;
                    model.Namasiswa = d.Fullname;
                    model.Kelastingkat = d.Kelas;
                    models.Add(model);
                }

                foreach (var dd in models)
                {
                    IEnumerable<Transaksi> t = db.Transaksis.OrderBy(x => x.TransId);
                    t = t.Where(x => x.Nosisda.Equals(dd.Nosisda));
                    foreach (var dt in t)
                    {
                        dd.biayaBM = dt.bayarBM.ToString();
                        dd.SSId = dt.SSId.ToString();
                        dd.nominal = dt.nominal;
                        dd.tglbayar = dt.tglbayar.ToString();
                    }
                    SchoolSupport dtss = db.SchoolSupports.Find(Convert.ToInt32(dd.SSId));
                    dd.SSId = dtss.JenisSS;
                }


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
                        data.Kelastingkat,
                        string.Format( "{0:#,#.00}", Convert.ToInt32(data.biayaBM) ),
                        data.SSId,
                        string.Format( "{0:#,#.00}", Convert.ToInt32(data.nominal) ),
                        data.tglbayar
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