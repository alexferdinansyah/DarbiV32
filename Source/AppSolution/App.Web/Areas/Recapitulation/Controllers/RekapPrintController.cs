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
    public class RekapPrintController : Controller
    {
        private DatabaseContext db = new DatabaseContext();
        // GET: Recapitulation/RekapPrint
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
        public ActionResult AjaxRekapPrint(JQueryDataTableParamModel param, SearchRekapBiayaMasuk m)
        {
            var QS = Request.QueryString;
            string Namasiswa = m.Namasiswa;
            DateTime tglbayar = Convert.ToDateTime(m.tglbayar).Date;

            List<RekapPrintVM> models = new List<RekapPrintVM>();
            List<string[]> listResult = new List<string[]>();
            String errorMessage = "";

            if (Namasiswa == "" || Namasiswa == null)
            {
                //jika tglbayar sebagai opsi pencarian
                if (tglbayar != null)
                {
                    IEnumerable<Transaksi> t = db.Transaksis.ToList();

                    foreach (var dd in t)
                    {
                        if (dd.tglbayar == tglbayar)
                        {
                            RekapPrintVM model = new RekapPrintVM();
                            model.Nosisda = dd.Nosisda;
                            model.Namasiswa = dd.Namasiswa;
                            model.Kelastingkat = dd.Kelastingkat;
                            model.cicilDaftarUlang = dd.cicilDaftarUlang.ToString();
                            model.biayaBM = dd.bayarBM.ToString();
                            model.bulanspp = dd.bulanspp.ToString();
                            model.bayarspp = dd.bayarspp.ToString();
                            //model.SSId = dd.SSId.ToString();
                            model.nominal = dd.nominal.ToString();
                            model.tglbayar = dd.tglbayar;
                            models.Add(model);

                        }
                    }
                }
                else
                {
                    //jika tglbayar pada tbl transaksi tidak ada yang sesuai dengan tglbayar pada pencarian 
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

            }
            else
            {
                //jika pencarian berdasarkan nama siswa
                try
                {
                    if (Namasiswa != null)
                    {
                        IEnumerable<Transaksi> t = db.Transaksis.ToList();

                        foreach (var dd in t)
                        {
                            if (dd.Namasiswa.Contains(Namasiswa))
                            {
                                RekapPrintVM model = new RekapPrintVM();
                                model.Nosisda = dd.Nosisda;
                                model.Namasiswa = dd.Namasiswa;
                                model.Kelastingkat = dd.Kelastingkat;
                                model.cicilDaftarUlang = dd.cicilDaftarUlang.ToString();
                                model.biayaBM = dd.bayarBM.ToString();
                                model.bulanspp = dd.bulanspp.ToString();
                                model.bayarspp = dd.bayarspp.ToString();
                                //model.SSId = dd.SSId.ToString();
                                model.nominal = dd.nominal.ToString();
                                model.tglbayar = dd.tglbayar;
                                models.Add(model);

                            }
                        }
                    }
                    else
                    {
                        //jika tglbayar pada tbl transaksi tidak ada yang sesuai dengan tglbayar pada pencarian 
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
                }
                catch (Exception ex)
                {
                    errorMessage = ex.Message;
                }

            }
            try { 
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
                        string.Format( "{0:#,#.00}", (data.cicilDaftarUlang == "" ? 0 : Convert.ToInt32(data.cicilDaftarUlang)) ),
                        string.Format( "{0:#,#.00}", (data.biayaBM == "" ? 0 : Convert.ToInt32(data.biayaBM)) ),
                        string.Format( "{0:#,#.00}", (data.bayarspp == "" ? 0 : Convert.ToInt32(data.bayarspp)) ),
                        data.bulanspp,
                        /*data.SSId,*/
                        string.Format( "{0:#,#.00}", Convert.ToInt32(data.nominal) ),
                        data.tglbayar.ToString()
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