﻿using System;
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
            List<SelectListItem> OpBM = new List<SelectListItem>()

            {
                new SelectListItem {Text="--- Pilih ---",Value="0",Selected=true},
                new SelectListItem {Text="Nama",Value="1"},
                new SelectListItem {Text="Tanggal",Value="2"},
            };
            Session["Opsi"] = model.Opsi;
            ViewBag.OpBM = OpBM;
            return View(model);
        }

        [HttpGet]
        public ActionResult AjaxRekapBiayaMasuk(JQueryDataTableParamModel param, SearchRekapBiayaMasuk m)
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

            List<RekapBiayaMasukVM> models = new List<RekapBiayaMasukVM>();
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
                            if (dd.bayarBM != 0)
                            {
                                RekapBiayaMasukVM model = new RekapBiayaMasukVM();
                                model.Nosisda = dd.Nosisda;
                                model.Namasiswa = dd.Namasiswa;
                                model.Kelastingkat = dd.Kelastingkat;
                                model.biayaBM = dd.bayarBM.ToString();
                                model.tglbayar = dd.tglbayar;
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

                        foreach (var dd in t)
                        {
                            if (dd.Namasiswa.Contains(Namasiswa)) ;
                            {
                                if (dd.bayarBM != 0)
                                {
                                    RekapBiayaMasukVM model = new RekapBiayaMasukVM();
                                    model.Nosisda = dd.Nosisda;
                                    model.Namasiswa = dd.Namasiswa;
                                    model.Kelastingkat = dd.Kelastingkat;
                                    model.biayaBM = dd.bayarBM.ToString();
                                    model.tglbayar = dd.tglbayar;
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
                        data.Nosisda,
                        data.Namasiswa,
                        data.Kelastingkat,
                        string.Format( "{0:#,#.00}", Convert.ToInt32(data.biayaBM) ),
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