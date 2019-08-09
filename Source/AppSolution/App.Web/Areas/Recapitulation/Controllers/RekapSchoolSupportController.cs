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


            /*
            * iMa : Refresh filtering option
            */
            Session["Opsi"] = model.Opsi;
            ViewBag.OpBM = OpBM;
            return View(model);
        }

        [HttpGet]
        public ActionResult AjaxRekapSchoolSupport(JQueryDataTableParamModel param, SearchRekapBiayaMasuk m)
        {
            if (Session["Opsi"] != null)
            {

                /*
                 * iMa : refresh filtering option
                 */
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


            List<RekapSchoolSupportVM> models = new List<RekapSchoolSupportVM>();
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
                            if (dd.SSId != null)
                            {
                                RekapSchoolSupportVM model = new RekapSchoolSupportVM();
                                model.Nosisda = dd.Nosisda;
                                model.Namasiswa = dd.Namasiswa;
                                model.Kelastingkat = dd.Kelastingkat;
                                model.tglbayar = dd.tglbayar;
                                models.Add(model);
                            }

                        }
                    }

                    /*
                     * iMa : filtering
                     */
                    int? isDel = null;
                    for (int j = 0; j < models.Count(); j++)
                    {
                        if (isDel != null)
                        {
                            j = models.Count() - 2;
                            models.Remove(models[Convert.ToInt32(isDel)]);
                        }
                        t = db.Transaksis.OrderBy(x => x.TransId);
                        t = t.Where(x => x.Nosisda.Equals(models[j].Nosisda));
                        if (t.Count() == 0)
                        {
                            if (j == models.Count() - 1)
                            {
                                models.Remove(models[j]);
                            }
                            else
                            {
                                isDel = j;
                            }
                        }
                        else
                        {
                            int eachsiswa = 0;
                            foreach (var dt in t)
                            {
                                if (dt.SSId != null)
                                {
                                    //RekapSPPVM mm = new RekapSPPVM();
                                    models[j].biayaBM = dt.bayarBM.ToString();
                                    models[j].SSId = dt.SSId.ToString();
                                    models[j].nominal = dt.nominal;
                                    models[j].tglbayar = Convert.ToDateTime(dt.tglbayar);
                                    eachsiswa++;
                                }
                            }
                            SchoolSupport dtss = db.SchoolSupports.Find(Convert.ToInt32(models[j].SSId));
                            models[j].SSName = dtss.JenisSS;
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

                    /*
                     * iMa : filtering
                     */
                    int? isDel = null;
                    for (int j = 0; j < models.Count(); j++)
                    {
                        if (isDel != null)
                        {
                            j = models.Count() - 2;
                            models.Remove(models[Convert.ToInt32(isDel)]);
                        }
                        IEnumerable<Transaksi> t = db.Transaksis.OrderBy(x => x.TransId);
                        t = t.Where(x => x.Nosisda.Equals(models[j].Nosisda));
                        if (t.Count() == 0)
                        {
                            if (j == models.Count() - 1)
                            {
                                models.Remove(models[j]);
                            }
                            else
                            {
                                isDel = j;
                            }
                        }
                        else
                        {
                            int eachsiswa = 0;
                            foreach (var dt in t)
                            {
                                if (dt.SSId != null)
                                {
                                    //RekapSPPVM mm = new RekapSPPVM();
                                    models[j].biayaBM = dt.bayarBM.ToString();
                                    models[j].SSId = dt.SSId.ToString();
                                    models[j].nominal = dt.nominal;
                                    models[j].tglbayar = Convert.ToDateTime(dt.tglbayar);
                                    eachsiswa++;
                                }
                            }
                            SchoolSupport dtss = db.SchoolSupports.Find(Convert.ToInt32(models[j].SSId));
                            models[j].SSName = dtss.JenisSS;
                        }
                    }

                    //foreach (var dd in models)
                    //{
                    //    IEnumerable<Transaksi> t = db.Transaksis.OrderBy(x => x.TransId);
                    //    t = t.Where(x => x.Nosisda.Equals(dd.Nosisda));
                    //    foreach (var dt in t)
                    //    {
                    //        dd.biayaBM = dt.bayarBM.ToString();
                    //        dd.SSId = dt.SSId.ToString();
                    //        dd.nominal = dt.nominal;
                    //        dd.tglbayar = Convert.ToDateTime(dt.tglbayar);
                    //    }
                    //    SchoolSupport dtss = db.SchoolSupports.Find(Convert.ToInt32(dd.SSId));
                    //    dd.SSId = dtss.JenisSS;
                    //}

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
                        data.SSId,
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