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

    public class RekapSchoolSupportController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: Recapitulation/RekapSchoolSupport
        public ActionResult Index(SearchRekapBiayaMasuk model = null)

        {
            List<SelectListItem> OpBM = new List<SelectListItem>()

            {
                new SelectListItem {Text="--- Pilih ---",Value="0",Selected=true},
                new SelectListItem {Text="School Support",Value="1"},
                new SelectListItem {Text="Tanggal",Value="2"},
                new SelectListItem {Text="Jenjang",Value="3"},
            };


            /*
            * iMa : Refresh filtering option
            */
            Session["Opsi"] = model.Opsi;
            Session["Opsiss"] = model.JenisSS;
            Session["Opsij"] = model.Jenjang;
            ViewBag.OpBM = OpBM;
            return View(model);
        }

        [HttpGet]
        public ActionResult AjaxRekapSchoolSupport(JQueryDataTableParamModel param, SearchRekapBiayaMasuk m)
        {
            var jss = "";
            var jjg = 0;
            var QS = Request.QueryString;
            string Namasiswa = QS["Namasiswa"];

            if (Namasiswa != null && Namasiswa != "")
            {
                Session["Opsi"] = null;
                m.tglbayar = null;
                m.JenjangId = 0;
                m.JenisSS = null;
            }

            if (Session["Opsi"] != null)
            {
                /*
                 * iMa : refresh filtering option
                 */
                m.Opsi = Session["Opsi"].ToString();
                if (m.Opsi == "School Support")
                {
                    m.tglbayar = null;
                    m.JenjangId = 0;
                    jjg = Convert.ToInt32(m.JenjangId);
                    jss = Session["Opsiss"].ToString();
                }
                else if (m.Opsi == "Tanggal")
                {
                    m.JenisSS = null;
                    m.JenjangId = 0;
                    jjg = Convert.ToInt32(m.JenjangId);
                }
                else
                {
                    m.tglbayar = null;
                    m.JenisSS = null;
                    jjg = Convert.ToInt32(Session["Opsij"]);
                }
            }

            
            //var jss = Session["Opsiss"];
            var jj = Session["Opsij"];
            //string JenisSs = m.JenisSS;
            DateTime tglbayar = Convert.ToDateTime(m.tglbayar).Date;
            DateTime tglnow = DateTime.Now.Date;
            var uname = User.Identity.GetUserName();

            List<RekapSchoolSupportVM> models = new List<RekapSchoolSupportVM>();
            List<string[]> listResult = new List<string[]>();
            String errorMessage = "";
            if ((m.tglbayar != null || jjg != null ) && jss != null)
            {
                if ((m.tglbayar != null || jjg != null) && jss != null)
                {
                    IEnumerable<Transaksi> tnow = db.Transaksis.ToList();
                    if ((tglnow != null) || (Namasiswa == null))
                    {
                        var D = tglnow.Date.ToShortDateString();
                        tnow = tnow.Where(x => x.tglbayar.ToString().Contains(tglnow.ToShortDateString()) || x.Namasiswa.Contains(Namasiswa.ToLower()));
                    }

                    foreach (var dd in tnow)
                    {
                        if (dd.tglbayar.ToString().Contains(tglnow.ToShortDateString()) && dd.JenisSS != null)
                        {
                            RekapSchoolSupportVM model = new RekapSchoolSupportVM();
                            model.tglbayar = dd.tglbayar;
                            model.Nosisda = dd.Nosisda;
                            model.Namasiswa = dd.Namasiswa;
                            model.Kelastingkat = dd.Kelastingkat;
                            model.Jenjang = dd.Jenjang;
                            model.SSName = dd.JenisSS;
                            model.nominal = dd.nominal;
                            model.tipebayar = dd.tipebayar;
                            model.Username = dd.Username;
                            models.Add(model);
                        }
                    }
                }

                else if (m.tglbayar != null)
                {     //jika tglbayar sebagai opsi pencarian
                    if (m.tglbayar != null)
                    {
                        IEnumerable<Transaksi> t = db.Transaksis.ToList();
                        if ((tglbayar != null) || (Namasiswa != null))
                        {
                            var D = tglbayar.Date.ToShortDateString();
                            t = t.Where(x => (x.tglbayar.ToString().Contains(tglbayar.ToShortDateString()) || x.Namasiswa.Contains(Namasiswa.ToLower())) && x.isCanceled.Equals(false));
                        }
                        foreach (var dd in t)
                        {
                            if (dd.tglbayar.ToString().Contains(tglbayar.ToShortDateString()))
                            {
                                if (dd.JenisSS != null)
                                {
                                    RekapSchoolSupportVM model = new RekapSchoolSupportVM();
                                    model.tglbayar = dd.tglbayar;
                                    model.Nosisda = dd.Nosisda;
                                    model.Namasiswa = dd.Namasiswa;
                                    model.Kelastingkat = dd.Kelastingkat;
                                    model.Jenjang = dd.Jenjang;
                                    model.SSName = dd.JenisSS;
                                    model.nominal = dd.nominal;
                                    model.tipebayar = dd.tipebayar;
                                    model.Username = dd.Username;
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
                            IEnumerable<Transaksi> tt = db.Transaksis.OrderBy(x => x.TransId);
                            tt = tt.Where(x => x.Nosisda.Equals(models[j].Nosisda));
                            if (tt.Count() == 0)
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
                                foreach (var dt in tt)
                                {
                                    if (dt.JenisSS != null)
                                    {
                                        if (tglbayar == dt.tglbayar)
                                        {
                                            //RekapSPPVM mm = new RekapSPPVM();
                                            //models[j].biayaBM = dt.bayarBM.ToString();
                                            models[j].tglbayar = Convert.ToDateTime(dt.tglbayar);
                                            models[j].SSId = dt.JenisSS;
                                            models[j].SSName = dt.JenisSS;
                                            models[j].nominal = dt.nominal;
                                            models[j].tipebayar = dt.tipebayar;
                                            models[j].Username = dt.Username;
                                            eachsiswa++;
                                        }

                                    }
                                }
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
            else if (Namasiswa != "")
            {
                jjg = 0;
                m.tglbayar = null;
                try
                {
                    if (Namasiswa != null)
                    {
                        IEnumerable<Transaksi> t = db.Transaksis.Where(M => M.Namasiswa.ToLower().Contains(Namasiswa.ToLower()) && M.isCanceled.Equals(false));
                        foreach (var dd in t)
                        {
                            if (dd.JenisSS != null)
                            {
                                RekapSchoolSupportVM model = new RekapSchoolSupportVM();
                                model.tglbayar = dd.tglbayar;
                                model.Nosisda = dd.Nosisda;
                                model.Namasiswa = dd.Namasiswa;
                                model.Kelastingkat = dd.Kelastingkat;
                                model.Jenjang = dd.Jenjang;
                                model.SSName = dd.JenisSS;
                                model.nominal = dd.nominal;
                                model.tipebayar = dd.tipebayar;
                                model.Username = dd.Username;
                                models.Add(model);
                            }
                        }
                    }

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
                                    //models[j].biayaBM = dt.bayarBM.ToString();
                                    models[j].tglbayar = Convert.ToDateTime(dt.tglbayar);
                                    models[j].SSId = dt.SSId.ToString();
                                    models[j].SSName = dt.JenisSS;
                                    models[j].nominal = dt.nominal;
                                    models[j].tipebayar = dt.tipebayar;
                                    models[j].Username = dt.Username;
                                    eachsiswa++;
                                }
                            }
                            SchoolSupport dtss = db.SchoolSupports.Find(Convert.ToInt32(models[j].SSName));
                            models[j].SSName = dtss.JenisSS;
                        }
                    }
                }
                catch (Exception ex)
                {
                    errorMessage = ex.Message;
                }
            }
            else if (jss != "")
            {
                try
                {
                    //jika pencarian berdasarkan Jenis SS
                    if (jss != null)
                    {
                        IEnumerable<SchoolSupport> infoss = db.SchoolSupports.Where(ss => ss.JenisSS.ToLower() == jss.ToString().ToLower());
                        var jName = "";
                        foreach (var ss in infoss)
                        {
                            jName = ss.JenisSS;
                            break;
                        }
                        // M.JenisSS.Equals[Sebelumnya]
                        IEnumerable<Transaksi> t = db.Transaksis.Where(M => M.JenisSS.ToLower().Contains(jss.ToLower())).ToList();
                        if (jss != null || Namasiswa != null)
                        {
                            t = t.Where(x => (x.JenisSS.ToLower().Contains(jss.ToLower()) && x.Namasiswa.Contains(Namasiswa.ToLower())) && x.isCanceled.Equals(false));
                        }

                        foreach (var dd in t)
                        {
                            if (dd.JenisSS.ToLower().Contains(jss.ToLower()))
                            {
                                if (dd.JenisSS != "-")
                                {
                                    RekapSchoolSupportVM model = new RekapSchoolSupportVM();
                                    model.tglbayar = dd.tglbayar;
                                    model.Nosisda = dd.Nosisda;
                                    model.Namasiswa = dd.Namasiswa;
                                    model.Kelastingkat = dd.Kelastingkat;
                                    model.Jenjang = dd.Jenjang;
                                    model.SSName = dd.JenisSS;
                                    model.nominal = dd.nominal;
                                    model.tipebayar = dd.tipebayar;
                                    model.Username = dd.Username;
                                    models.Add(model);
                                }
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
                                    //models[j].biayaBM = dt.bayarBM.ToString();
                                    models[j].tglbayar = Convert.ToDateTime(dt.tglbayar);
                                    models[j].SSId = dt.SSId.ToString();
                                    models[j].SSName = dt.JenisSS;
                                    models[j].nominal = dt.nominal;
                                    models[j].tipebayar = dt.tipebayar;
                                    models[j].Username = dt.Username;
                                    eachsiswa++;
                                }
                            }
                            SchoolSupport dtss = db.SchoolSupports.Find(Convert.ToInt32(models[j].SSName));
                            models[j].SSName = dtss.JenisSS;
                        }
                    }
                }
                catch (Exception ex)
                {
                    errorMessage = ex.Message;
                }
            }
            else
            {
                if (jjg != null)
                {
                    //Pencarian Berdasarkan Jenjang
                    IEnumerable<Jenjang> infoJ = db.Jenjangs.Where(n => n.JenjangId == jjg);
                    var jName = "";
                    foreach (var i in infoJ)
                    {
                        jName = i.JenjangName;
                        break;
                    }
                    IEnumerable<Transaksi> t = db.Transaksis.Where(M => M.Jenjang.Equals(jName)).ToList();
                    if ((jName != null) && (Namasiswa != null))
                    {
                        t = t.Where(x => x.Jenjang.Contains(jName) && x.Namasiswa.Contains(Namasiswa.ToLower()) && x.isCanceled.Equals(false));
                    }
                    foreach (var dd in t)
                    {
                        if (dd.Jenjang.Contains(jName))
                        {
                            if (dd.JenisSS != "" && dd.nominal != null)
                            {
                                RekapSchoolSupportVM model = new RekapSchoolSupportVM();
                                model.tglbayar = dd.tglbayar;
                                model.Nosisda = dd.Nosisda;
                                model.Namasiswa = dd.Namasiswa;
                                model.Kelastingkat = dd.Kelastingkat;
                                model.Jenjang = dd.Jenjang;
                                model.SSName = dd.JenisSS;
                                model.nominal = dd.nominal;
                                model.tipebayar = dd.tipebayar;
                                model.Username = dd.Username;
                                models.Add(model);
                            }
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
                                //models[j].biayaBM = dt.bayarBM.ToString();
                                models[j].tglbayar = Convert.ToDateTime(dt.tglbayar);
                                models[j].SSId = dt.SSId.ToString();
                                models[j].SSName = dt.JenisSS;
                                models[j].nominal = dt.nominal;
                                models[j].tipebayar = dt.tipebayar;
                                models[j].Username = dt.Username;
                                eachsiswa++;
                            }
                        }
                        //SchoolSupport dtss = db.SchoolSupports.Find(Convert.ToInt32(models[j].SSName));
                        //models[j].SSId = dtss.JenisSS;
                    }
                }
            }

            //Jika ada hasil pencarian baik berdasar SS,Jenjang maupun tglbayar
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
                        //string.Format( "{0:#,#.00}", Convert.ToInt32(data.biayaBM) ),
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