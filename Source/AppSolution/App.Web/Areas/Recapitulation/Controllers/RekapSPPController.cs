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
    public class RekapSPPController : Controller
    {
        private DatabaseContext db = new DatabaseContext();
        // GET: Recapitulation/RekapSPP
        public ActionResult Index(SearchRekapBiayaMasuk model = null)
        {
            List<SelectListItem> OpSrc = new List<SelectListItem>()

            {
                new SelectListItem {Text="--- Pilih ---",Value="0",Selected=true},
                new SelectListItem {Text="Nama",Value="1"},
                new SelectListItem {Text="Tanggal",Value="2"},
            };
            Session["Opsi"] = model.Opsi;
            ViewBag.OpSrc = OpSrc;
            return View(model);
        }

        [HttpGet]
        public ActionResult AjaxSPP(JQueryDataTableParamModel param, SearchRekapBiayaMasuk m)
        {
            if (Session["Opsi"] != null)
            {
                m.Opsi = Session["Opsi"].ToString();
                if (m.Opsi == "Nama")
                {
                    m.tglbayar = null;
                }
                else
                {
                    m.Namasiswa = null;
                }
            }

            var QS = Request.QueryString;
            string Namasiswa = m.Namasiswa;
            DateTime tglbayar = Convert.ToDateTime(m.tglbayar).Date;

            List<RekapSPPVM> models = new List<RekapSPPVM>();
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
                            if (dd.bulanspp != null)
                            {
                                RekapSPPVM model = new RekapSPPVM();
                                model.Nosisda = dd.Nosisda;
                                model.Namasiswa = dd.Namasiswa;
                                model.Kelastingkat = dd.Kelastingkat;
                                model.bulanspp = dd.bulanspp.ToString();
                                model.bayarspp = dd.bayarspp.ToString();
                                model.tglbayar = dd.tglbayar;
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
            else
            {
                try
                {
                    IEnumerable<Siswa> datasiswa = db.Siswas.Where(x => x.Fullname.ToLower().Contains(Namasiswa.ToLower()));
                    string Nosisda = "";
                    foreach (var d in datasiswa)
                    {
                        RekapSPPVM model = new RekapSPPVM();
                        model.Nosisda = d.Nosisda;
                        model.Namasiswa = d.Fullname;
                        model.Kelastingkat = d.Kelas;
                        models.Add(model);
                    }
                    for (int j = 0; j < models.Count(); j++)
                    {
                        IEnumerable<Transaksi> t = db.Transaksis.OrderBy(x => x.TransId);
                        t = t.Where(x => x.Nosisda.Equals(models[j].Nosisda));
                        if(t.Count() == 0)
                        {
                            models.Remove(models[j]);
                        }
                        foreach (var dt in t)
                        {
                            if (dt.bulanspp != null)
                            {
                                //RekapSPPVM mm = new RekapSPPVM();
                                models[j].bulanspp = dt.bulanspp.ToString();
                                models[j].bayarspp = dt.bayarspp.ToString();
                                models[j].tglbayar = Convert.ToDateTime(dt.tglbayar);
                                //mm = models[j];
                                
                                //models.Insert(0, mm);
                            }
                            else
                            {
                                //models.Remove(models[j]);
                            }
                        }
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
                        data.Nosisda,
                        data.Namasiswa,
                        data.Kelastingkat,
                        data.bulanspp,
                        data.bayarspp,
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