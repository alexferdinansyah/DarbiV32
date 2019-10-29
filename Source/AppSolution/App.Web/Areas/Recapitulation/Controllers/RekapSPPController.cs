using App.Entities.DataAccessLayer;
using App.Entities.Models;
using App.Web.Areas.Recapitulation.Models;
using App.Web.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace App.Web.Areas.Recapitulation.Controllers
{
    public class RekapSPPController : Controller
    {
        private DatabaseContext db = new DatabaseContext();
        // GET: Recapitulation/RekapSPP
        public ActionResult Index(SearchRekapBiayaMasuk model = null)
        {
            List<SelectListItem> OpSrc = new List<SelectListItem>()

            {
                new SelectListItem {Text="--- Pilih ---",Value="0",Selected=true},
                new SelectListItem {Text="Jenjang",Value="1"},
                new SelectListItem {Text="Tanggal",Value="2"},
            };
            Session["Opsi"] = model.Opsi;
            Session["valOpsi"] = model.Jenjang;
            ViewBag.OpSrc = OpSrc;
            return View(model);
        }

        //info History Biaya SPP per periode
        public ActionResult History(string nosisda)
        {
            if (nosisda == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IEnumerable<Transaksi> trans = null;
            if (db.Transaksis.Count() != 0)
            {
                trans = db.Transaksis.Where(x => x.Nosisda.Equals(nosisda));
            }
            if (trans == null)
            {
                return HttpNotFound();
            }
            return View(trans);
        }

        //Data Table SPP
        [HttpGet]
        public ActionResult AjaxSPP(JQueryDataTableParamModel param, SearchRekapBiayaMasuk m)
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
            
            string Namasiswa = m.Namasiswa;
            if(Namasiswa == null)
            {
                Namasiswa = "";
            }

            //string jenj = m.Jenjang.ToString();
            //if(jenj == null)
            //{
            //    jenj = "";
            //}
            


            DateTime tglbayar = Convert.ToDateTime(m.tglbayar).Date;
            var uname = User.Identity.GetUserName();

            List<RekapSPPVM> models = new List<RekapSPPVM>();
            List<string[]> listResult = new List<string[]>();
            String errorMessage = "";
            if (Jid == 0 || Jid == null)
            {
                //jika tglbayar sebagai opsi pencarian
                if (tglbayar != null)
                {
                    IEnumerable<Transaksi> t = db.Transaksis.ToList();
                    if ((tglbayar != null)/* || (jenj != null)*/ || (Namasiswa != null))
                    {
                        var D = tglbayar.Date.ToShortDateString();
                        t = t.Where(x => x.tglbayar.ToString().Contains(tglbayar.ToShortDateString()) || x.Namasiswa.Contains(Namasiswa));
                    }

                    foreach (var dd in t)
                    {
                        if (dd.tglbayar.ToString().Contains(tglbayar.ToShortDateString()))
                        {
                            if (dd.bulanspp != null && dd.bulanspp != "-")
                            {
                                RekapSPPVM model = new RekapSPPVM();
                                model.tglbayar = dd.tglbayar;
                                model.Nosisda = dd.Nosisda;
                                model.Namasiswa = dd.Namasiswa;
                                model.Kelastingkat = dd.Kelastingkat;
                                model.Jenjang = dd.Jenjang;
                                model.bulanspp = dd.bulanspp.ToString();
                                model.bayarspp = dd.bayarspp.ToString();
                                model.tipebayar = dd.tipebayar;
                                model.Username = dd.Username;
                                models.Add(model);
                            }

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
            // jika pencarian berdasarkan nama
            if ((Jid == null || Jid == 0) && m.tglbayar == null)
            {
                try
                {
                    if (Namasiswa != null)
                    {
                        IEnumerable<Transaksi> t = db.Transaksis.Where(M => M.Namasiswa.ToLower().Contains(Namasiswa.ToLower()) && M.isCanceled.Equals(false));
                        

                        foreach (var dd in t)
                        {
                            if (dd.bulanspp != null && dd.bulanspp != "-")
                            {
                                RekapSPPVM model = new RekapSPPVM();
                                model.tglbayar = dd.tglbayar;
                                model.Nosisda = dd.Nosisda;
                                model.Namasiswa = dd.Namasiswa;
                                model.Kelastingkat = dd.Kelastingkat;
                                model.Jenjang = dd.Jenjang;
                                model.bulanspp = dd.bulanspp.ToString();
                                model.bayarspp = dd.bayarspp.ToString();
                                model.tipebayar = dd.tipebayar;
                                model.Username = dd.Username;
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
                try
                {
                    if (Jid != null)
                    {
                        //pencarian berdasarkan jenjang
                        IEnumerable<Jenjang> infoJ = db.Jenjangs.Where(n => n.JenjangId == Jid);
                        var jName = "";
                        foreach (var i in infoJ)
                        {
                            jName = i.JenjangName;
                            break;
                        }

                        IEnumerable<Transaksi> t = db.Transaksis.Where(M => M.Jenjang.Equals(jName)).ToList();
                        if ((jName != null) || (Namasiswa != null))
                        {
                            //t = t.Where(x => x.Jenjang.Equals(jName) || (x.Namasiswa.Contains(Namasiswa)) || x.isCanceled.Equals(false));
                            t = t.Where(x => (x.Jenjang.Equals(jName) || x.Namasiswa.Contains(Namasiswa)) && x.isCanceled.Equals(false));
                        }

                        foreach (var dd in t)
                        {
                            if (dd.Jenjang.Contains(jName))
                            {
                                if (dd.bulanspp != null && dd.bulanspp != "-")
                                {
                                    RekapSPPVM model = new RekapSPPVM();
                                    model.tglbayar = dd.tglbayar;
                                    model.Nosisda = dd.Nosisda;
                                    model.Namasiswa = dd.Namasiswa;
                                    model.Kelastingkat = dd.Kelastingkat;
                                    model.Jenjang = dd.Jenjang;
                                    model.bulanspp = dd.bulanspp.ToString();
                                    model.bayarspp = dd.bayarspp.ToString();
                                    model.tipebayar = dd.tipebayar;
                                    model.Username = dd.Username;
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
                        data.bulanspp,
                        data.bayarspp,
                        data.tipebayar,
                        data.Username
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