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
using App.Web.Areas.Transaction.Models;
using App.Entities;

namespace App.Web.Areas.Transaction.Controllers
{
    public class TransactionController : Controller
    {
        private DatabaseContext db = new DatabaseContext();
        // GET: Transaction/Transaction
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
        public ActionResult AjaxTrans(JQueryDataTableParamModel param, TransactionSearchFormVM m)
        {

            var QS = Request.QueryString;
            var Fullname = System.Web.HttpContext.Current.Session["NamaSiswa"];
            //Boolean IsActive = (QS["IsActive"] == "false" ? false : true);

            List<string[]> listResult = new List<string[]>();
            String errorMessage = "";
            if (Convert.ToString(Fullname) == "" || Fullname == null)
            {
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
            try
            {
                IEnumerable<Siswa> Query = db.Siswas;
                Query = Query.Where(x => x.Fullname.ToLower().Contains(Convert.ToString(Fullname)));

                //Query = Query.Where(x => x.IsActive == IsActive);
                //IEnumerable<Transaksi> Query = db.Transaksis;
                //foreach(var d in Querys)
                //{
                //    Query.Where(x => x.Nosisda.Equals(d.Nosisda));
                //}

                int TotalRecord = Query.Count();

                var OrderedQuery = Query.OrderBy(x => x.SiswaId);

                int pageSize = param.iDisplayLength;
                int pageNumber = param.iDisplayStart == 0 ? 1 : (param.iDisplayStart / param.iDisplayLength) + 1; ;
                var PagedQuery = OrderedQuery.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();

                int i = 0;
                foreach (var data in PagedQuery)
                {

                    i++;
                    listResult.Add(new string[]
                    {
                        i.ToString(),
                        data.Nosisda,
                        data.Fullname,
                        data.Periode,
                        data.Kelas + "-" + data.Kelas,
                        data.Nosisda.ToString()
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

        //GET : Transaction/Transaction/Detail Transaksi
        public ActionResult Details(string nosisda)
        {
            if (nosisda == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IEnumerable<Transaksi> dttrans = null;
            if (db.Transaksis.Count() != 0)
            {
                dttrans = db.Transaksis.Where(x => x.Nosisda.Equals(nosisda)).OrderByDescending(x => x.tglbayar);
            }

            Transaksi tr = new Transaksi();
            if (dttrans == null)
            {
                return HttpNotFound();
            }
            else
            {
                foreach (var d in dttrans)
                {
                    tr.Nosisda = d.Nosisda;
                    tr.totalBM = d.totalBM;
                    tr.bayarBM = d.bayarBM;
                    tr.periode = d.periode;
                    tr.bulanspp = d.bulanspp;
                    tr.bayarspp = d.bayarspp;
                    tr.tipebayar = d.tipebayar;
                    tr.tglbayar = d.tglbayar;
                    if (d.tipebayar.ToLower() != "cash")
                    {
                        tr.tgltransfer = d.tgltransfer;
                        tr.namabank = d.namabank;
                    }
                }
                IEnumerable<Siswa> dts = db.Siswas.Where(x => x.Nosisda.Equals(tr.Nosisda));
                foreach (var d1 in dts)
                {
                    tr.Namasiswa = d1.Fullname;
                    tr.Kelastingkat = d1.Kelas;
                }
            }
            return View(tr);
        }

        //GET : Transaction/Transaction/Lakukan Transaksi
        public ActionResult FormTrans(TransactionFormCreateVM mod)
        {
            //info siswa
            IEnumerable<Siswa> dts = db.Siswas.Where(x => x.Nosisda.Equals(mod.Nosisda));
            string[] keltingkat = null;
            string tkt = "";
            foreach (var d in dts)
            {
                mod.Namasiswa = d.Fullname;
                mod.periode = d.Periode;
                if(d.Kelas != null || d.Kelas != "")
                {
                    keltingkat = d.Kelas.Split(' ');
                    tkt = keltingkat[0];
                }
            }

            //info tingkat to get info biaya
            IEnumerable<Tingkat> dtTingkat = db.Tingkats.Where(x => x.Namatingkat.Equals(tkt));
            int idtingkat = 0;
            foreach(var t in dtTingkat)
            {
                idtingkat = t.TingkatId;
            }

            //info biaya
            IEnumerable<Biaya> dtb = db.Biayas;
            foreach (var dd in dtb)
            {
                if(dd.TingkatId == idtingkat)
                {
                    if (dd.KatBiaya == "Biaya Masuk")
                    {
                        mod.totalBM = dd.NomBiaya;
                    }
                    if (dd.KatBiaya == "SPP" || dd.KatBiaya == "KS")
                    {
                        int totalSPP = Convert.ToInt32(mod.bayarspp) + Convert.ToInt32(dd.NomBiaya);
                        mod.bayarspp = totalSPP.ToString();
                    }
                }
                
            }

            //info paid BM (BM yang sudah dibayarkan/cicilan BM)
            IEnumerable<Transaksi> dtts = db.Transaksis.Where(x => x.Nosisda.Equals(mod.Nosisda));
            foreach(var t in dtts)
            {
                mod.paidBM = t.bayarBM;
            }

            return View(mod);
        }
    }
}