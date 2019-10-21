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
using Microsoft.AspNet.Identity;

namespace App.Web.Areas.Recapitulation.Controllers
{
    public class RekapPrintController : Controller
    {
        private DatabaseContext db = new DatabaseContext();
        // GET: Recapitulation/RekapPrint
        public ActionResult Index(SearchRekapBiayaMasuk model = null)

        {
            if (model == null)
            {
                model = new SearchRekapBiayaMasuk();
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult AjaxRekapPrint(JQueryDataTableParamModel param, SearchRekapBiayaMasuk m)
        {
            var QS = Request.QueryString;
            string Namasiswa = m.Namasiswa;
            DateTime tglbayar = Convert.ToDateTime(m.tglbayar).Date;
            var uname = User.Identity.GetUserName();

            List<RekapPrintVM> models = new List<RekapPrintVM>();
            List<string[]> listResult = new List<string[]>();
            String errorMessage = "";

            if (Namasiswa == "" || Namasiswa == null)
            {
                //jika tglbayar sebagai opsi pencarian
                if (tglbayar != null)
                {
                    IEnumerable<Transaksi> t = db.Transaksis.ToList();
                    if ((tglbayar != null) || (Namasiswa != null))
                    {
                        var D = tglbayar.Date.ToShortDateString();
                        t = t.Where(x => x.tglbayar.ToString().Contains(tglbayar.ToShortDateString()) || x.Namasiswa.Contains(Namasiswa)); ;
                    }
                    foreach (var dd in t)
                    {
                        if (dd.tglbayar.ToString().Contains(tglbayar.ToShortDateString()))
                        {
                            if (tglbayar == dd.tglbayar)
                            {
                                RekapPrintVM model = new RekapPrintVM();
                                model.tglbayar = dd.tglbayar;
                                model.Nosisda = dd.Nosisda;
                                model.Namasiswa = dd.Namasiswa;
                                model.Kelastingkat = dd.Kelastingkat;
                                model.cicilDaftarUlang = dd.cicilDaftarUlang.ToString();
                                model.biayaBM = dd.bayarBM.ToString();
                                model.bulanspp = dd.bulanspp.ToString();
                                model.bayarspp = dd.bayarspp.ToString();
                                model.SSName = dd.JenisSS;
                                model.nominal = dd.nominal;
                                model.tipebayar = dd.tipebayar;
                                model.Username = uname;
                                models.Add(model);
                            }
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
                        if ((Namasiswa != null) || (tglbayar != null))
                        {
                            t = t.Where(x => x.Namasiswa.Contains(Namasiswa) || x.tglbayar.Equals(tglbayar));
                        }

                        foreach (var dd in t)
                        {
                            if (dd.Namasiswa.ToLower().Contains(Namasiswa.ToLower()))
                            {
                                RekapPrintVM model = new RekapPrintVM();
                                model.tglbayar = dd.tglbayar;
                                model.Nosisda = dd.Nosisda;
                                model.Namasiswa = dd.Namasiswa;
                                model.Kelastingkat = dd.Kelastingkat;
                                model.cicilDaftarUlang = dd.cicilDaftarUlang.ToString();
                                model.biayaBM = dd.bayarBM.ToString();
                                model.bulanspp = dd.bulanspp.ToString();
                                model.bayarspp = dd.bayarspp.ToString();
                                model.SSName = dd.JenisSS;
                                model.nominal = dd.nominal;
                                model.tipebayar = dd.tipebayar;
                                model.Username = uname;
                                models.Add(model);

                            }
                        }
                    }
                    else
                    {
                        //jika nama pada tbl transaksi tidak ada yang sesuai dengan nama pada pencarian 
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
                        data.tglbayar.ToString(),
                        data.Nosisda,
                        data.Namasiswa,
                        data.Kelastingkat,
                        data.cicilDaftarUlang,
                        data.biayaBM,
                        data.bayarspp,
                        data.bulanspp,
                        data.SSName,
                        data.nominal,
                        data.tipebayar,
                        data.Username,
                    });
        
                }
                return Json(new
                {
                    sEcho = param.sEcho,
                    iTotalRecords = 0,
                    iTotalDisplayRecords = 0,
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