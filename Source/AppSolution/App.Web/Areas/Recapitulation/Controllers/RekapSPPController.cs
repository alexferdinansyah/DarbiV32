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
using App.Web.Areas.Transaction.Models;
using App.Entities;

namespace App.Web.Areas.Recapitulation.Controllers
{
    public class RekapSPPController : Controller
    {
        private DatabaseContext db = new DatabaseContext();
        // GET: Recapitulation/RekapSPP
        public ActionResult Index(TransactionSearchFormVM model = null)
        {
            if (model == null)
            {
                model = new TransactionSearchFormVM();
            }
            System.Web.HttpContext.Current.Session["NamaSiswa"] = model.NamaSiswa;
            return View(model);
        }

        [HttpGet]
        public ActionResult AjaxSPP(JQueryDataTableParamModel param, TransactionSearchFormVM m)
        {
            var QS = Request.QueryString;
            string NamaSiswa = "";
            if(System.Web.HttpContext.Current.Session["NamaSiswa"] != null)
            {
                NamaSiswa = Convert.ToString(System.Web.HttpContext.Current.Session["NamaSiswa"]);
            }

            List<TransactionFormCreateVM> models = new List<TransactionFormCreateVM>();
            List<string[]> listResult = new List<string[]>();
            String errorMessage = "";
            if (NamaSiswa == "")
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
                IEnumerable<Siswa> datasiswa = db.Siswas.Where(x => x.Fullname.ToLower().Contains(NamaSiswa.ToLower()));
                string Nosisda = "";
                foreach (var d in datasiswa)
                {
                    TransactionFormCreateVM model = new TransactionFormCreateVM();
                    model.Nosisda = d.Nosisda;
                    model.Namasiswa = d.Fullname;
                    model.periode = d.Periode;
                    model.Kelastingkat = d.Kelas;
                    models.Add(model);
                }

                foreach (var dd in models)
                {
                    IEnumerable<Transaksi> t = db.Transaksis.OrderBy(x => x.TransId);
                    t = t.Where(x => x.Nosisda.Equals(dd.Nosisda));
                    foreach (var dt in t)
                    {
                        dd.bulanspp = dt.bulanspp.ToString();
                        dd.bayarspp = dt.bayarspp.ToString();
                    }
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
                        data.periode,
                        data.Kelastingkat,
                        data.bulanspp,
                        data.bayarspp
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