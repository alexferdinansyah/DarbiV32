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
    public class RekapDaftarUlangController : Controller
    {
        private DatabaseContext db = new DatabaseContext();
        // GET: Recapitulation/RekapDaftarUlang
        public ActionResult Index(SearchRekapBiayaMasuk model = null)

        {
            List<SelectListItem> OpBM = new List<SelectListItem>()

            {
                new SelectListItem {Text="--- Pilih ---",Value="0",Selected=true},
                new SelectListItem {Text="Jenjang",Value="1"},
                new SelectListItem {Text="Tanggal",Value="2"},
            };
            Session["Opsi"] = model.Opsi;
            Session["valOpsi"] = model.Jenjang;
            ViewBag.OpBM = OpBM;
            return View(model);
        }

        [HttpGet]
        public ActionResult AjaxRekapDU(JQueryDataTableParamModel param, SearchRekapBiayaMasuk m)
        {
            var Jid = 0;
            if (Session["Opsi"] != null)
            {
                m.Opsi = Session["Opsi"].ToString();
                if (m.Opsi == "Jenjang")
                {
                    m.tglbayar = null;
                    Jid = Convert.ToInt32(Session["valOpsi"]);
                }
                else
                {
                    m.Jenjang = null;
                }
            }

            var QS = Request.QueryString;
            //var Jid = Convert.ToInt32(Session["valOpsi"]);
            DateTime tglbayar = Convert.ToDateTime(m.tglbayar).Date;
            string Namasiswa = QS["Namasiswa"];
            var uname = User.Identity.GetUserName();

            List<RekapDaftarUlangVM> models = new List<RekapDaftarUlangVM>();
            List<string[]> listResult = new List<string[]>();
            String errorMessage = "";
            if (Jid == 0 || Jid == null)
            {
                //jika tglbayar sebagai opsi pencarian
                if (tglbayar != null)
                {
                    IEnumerable<Transaksi> t = db.Transaksis.ToList();
                    if ((tglbayar != null) || (Namasiswa != ""))
                    {
                        var D = tglbayar.Date.ToShortDateString();
                        t = t.Where(x => x.tglbayar.ToString().Contains(tglbayar.ToShortDateString()) || x.Namasiswa.Contains(Namasiswa));

                    }
                    foreach (var dd in t)
                    {
                        if (dd.tglbayar.ToString().Contains(tglbayar.ToShortDateString()))
                        {
                            if (dd.cicilDaftarUlang != null)
                            {
                                RekapDaftarUlangVM model = new RekapDaftarUlangVM();
                                model.tglbayar = dd.tglbayar;
                                model.Nosisda = dd.Nosisda;
                                model.Namasiswa = dd.Namasiswa;
                                model.Kelastingkat = dd.Kelastingkat;
                                model.Jenjang = dd.Jenjang;
                                model.cicilDaftarUlang = dd.cicilDaftarUlang.ToString();
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
            // jika pencarian berdasarkan nama
            if ((Jid == null || Jid == 0) && m.tglbayar == null)
            {
                try
                {
                    if (Namasiswa != null)
                    {
                        IEnumerable<Transaksi> t = db.Transaksis.Where(M => M.Namasiswa.ToLower().Contains(Namasiswa.ToLower()));
                        foreach (var dd in t)
                        {
                            if (dd.cicilDaftarUlang != null)
                            {
                                RekapDaftarUlangVM model = new RekapDaftarUlangVM();
                                model.tglbayar = dd.tglbayar;
                                model.Nosisda = dd.Nosisda;
                                model.Namasiswa = dd.Namasiswa;
                                model.Kelastingkat = dd.Kelastingkat;
                                model.Jenjang = dd.Jenjang;
                                model.cicilDaftarUlang = dd.cicilDaftarUlang.ToString();
                                model.tipebayar = dd.tipebayar;
                                model.Username = uname;
                                models.Add(model);
                            }
                        }
                    }
                    else
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
                }
                catch (Exception ex)
                {
                    errorMessage = ex.Message;
                }
            }
            else
            {
                //jika pencarian berdasarkan jenjang
                try
                {
                    if (Jid != null)
                    {
                        //Search for jenjangName in Jenjang
                        IEnumerable<Jenjang> infoJ = db.Jenjangs.Where(n => n.JenjangId == Jid);
                        var jName = "";
                        foreach (var i in infoJ)
                        {
                            jName = i.JenjangName;
                            break;
                        }
                        IEnumerable<Transaksi> t = db.Transaksis.Where(M => M.Jenjang.Equals(jName) && (M.Jenjang.Equals("PG") || M.Jenjang.Equals("TK A")) && M.cicilDaftarUlang != null).ToList();
                        if ((jName != null ) || Namasiswa != null)
                        {
                            t = t.Where(x => x.Jenjang.Contains(jName) || x.Namasiswa.Contains(Namasiswa.ToLower()));
                        }

                        foreach (var dd in t)
                        {
                            if (dd.Jenjang.Contains(jName))
                            {
                                if (dd.cicilDaftarUlang != 0)
                                {
                                    RekapDaftarUlangVM model = new RekapDaftarUlangVM();
                                    model.tglbayar = dd.tglbayar;
                                    model.Nosisda = dd.Nosisda;
                                    model.Namasiswa = dd.Namasiswa;
                                    model.Kelastingkat = dd.Kelastingkat;
                                    model.Jenjang = dd.Jenjang;
                                    model.cicilDaftarUlang = dd.cicilDaftarUlang.ToString();
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
                catch (Exception ex)
                {
                    errorMessage = ex.Message;
                }
            }

            //Jika ada hasil pencarian baik berdasar nama maupun tglbayar
            try
            {
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
                        data.Jenjang,
                        data.cicilDaftarUlang,
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