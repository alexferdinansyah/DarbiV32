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
using Microsoft.AspNet.Identity;

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

        public ActionResult PeriodeAjax(string input)
        {
            var inputarray = input.Split('.');
            var periode = inputarray[0];
            var nosisda = inputarray[1];

            IEnumerable<Transaksi> spp = db.Transaksis.Where(x => x.Nosisda.Equals(nosisda));
            string[] vv = new string[spp.Count()];
            var bln = "";
            for (int i = 0; i < spp.Count(); i++)
            {
                if ((spp.ToList()[i].bulanspp != "-" || spp.ToList()[i].bulanspp != null) && spp.ToList()[i].periode == periode)
                {
                    if (spp.ToList()[i].isCanceled == false)
                    {
                        vv[i] = spp.ToList()[i].bulanspp;
                    }
                    //break;
                }
            }
            if (vv != null)
            {
                bln = String.Join(",", vv);
            }

            var bulanBayar = bln.Split(',');
            List<string> blnBayar = new List<string>(bulanBayar);

            //list bulanspp for multiselect
            List<Bulan> bulans = db.Bulans.Where(s => !blnBayar.Contains(s.namaBulan)).ToList();

            return Json(bulans, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FirstAjax(string input)
        {
            //2-9 Mekah : idSchoolSupport-kelastingkat
            var inputarray = input.Split('-');
            var kt = inputarray[1].Split(' ');
            var ssid = inputarray[0].Split(',');
            var ca = inputarray[2].Split(',');
            var aj = inputarray[3].Split(',');
            string tkt = "";
            tkt = kt[0].ToString();
            
            Tingkat ts = db.Tingkats.Where(x => x.Namatingkat.Equals(tkt)).FirstOrDefault();
            IEnumerable<Biaya> b = null;

            Biaya by = null;

            var total = 0;
            var totalca = 0;
            var totalaj = 0;
            if (inputarray[0] != "null")
            {
                for (int i = 0; i < ssid.Count(); i++)
                {
                    b = db.Biayas.Where(x => x.TingkatId == ts.TingkatId);
                    SchoolSupport ss = db.SchoolSupports.Find(Convert.ToInt32(ssid[i]));
                    //jika ssid catering
                    if (ss.SsId.Equals(10))
                    {
                        b = b.Where(x => x.JenisBiaya.ToLower().Equals(ss.JenisSS.ToLower()) && x.KatBiaya.ToLower().Equals("school support"));
                        by = b.FirstOrDefault();
                        totalca = Convert.ToInt32(by.NomBiaya);
                        if (ca[0] == "null") totalca *= 0; else totalca *= ca.Count();
                    }
                    //jika ssid antarjemput
                    else if (ss.SsId.Equals(11))
                    {
                        b = b.Where(x => x.JenisBiaya.ToLower().Equals(ss.JenisSS.ToLower()) && x.KatBiaya.ToLower().Equals("school support"));
                        by = b.FirstOrDefault();
                        totalaj = Convert.ToInt32(by.NomBiaya);
                        if (aj[0] == "null") totalaj *= 0; else totalaj *= aj.Count();
                    }
                    else
                    {
                        b = b.Where(x => x.JenisBiaya.ToLower().Equals(ss.JenisSS.ToLower()) && x.KatBiaya.ToLower().Equals("school support"));
                        by = b.FirstOrDefault();
                        total += Convert.ToInt32(by.NomBiaya);
                    }
                }
            }
            return Json(total + totalca + totalaj, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SPPAjax(string input)
        {
       
            var inputarray = input.Split('-');
            var blnspp = inputarray[0].Split(',');
            var kt = inputarray[1].Split(' ');
            var nosisda = inputarray[2];
            string tkt = "";
            tkt = kt[0].ToString();

            Tingkat ts = db.Tingkats.Where(x => x.Namatingkat.Equals(tkt)).FirstOrDefault();
            IEnumerable<Biaya> b = null;

            IEnumerable<Siswa> dts = db.Siswas.Where(x => x.Nosisda.Equals(nosisda));
            string disc = "";
            string discspp = "";
            int diskonbm = 0;
            int diskonspp = 0;
            foreach (var d in dts)
            {
                disc = d.TypeDiscAdm;
                discspp = d.TypeDisc;
                diskonbm = Convert.ToInt32(d.NomDiscAdm);
                diskonspp = Convert.ToInt32(d.NomDisc);
            }

            Biaya by = null;

            var spp = 0;
            var totalspp = 0;
            var totalks = 0;
            if (inputarray[0] != "null")
            {
                b = db.Biayas.Where(x => x.TingkatId == ts.TingkatId);
                b = b.Where(x => x.JenisBiaya.ToLower().Equals("spp") && x.KatBiaya.ToLower().Equals("spp"));
                by = b.FirstOrDefault();

                if (discspp == "Rp")
                {
                    spp = Convert.ToInt32(by.NomBiaya) - (diskonspp);
                }
                if (discspp == "%")
                {
                    spp = Convert.ToInt32(by.NomBiaya) - (Convert.ToInt32(by.NomBiaya) * (diskonspp) / 100);
                }
                else if (discspp == "" || discspp == null)
                    spp = Convert.ToInt32(by.NomBiaya);

                totalspp += (spp * blnspp.Count());
            }
            if (inputarray[0] != "null")
            {
                b = db.Biayas.Where(x => x.TingkatId == ts.TingkatId);
                b = b.Where(x => x.JenisBiaya.ToLower().Equals("ks") && x.KatBiaya.ToLower().Equals("spp"));
                by = b.FirstOrDefault();

                totalks += ((Convert.ToInt32(by.NomBiaya)) * blnspp.Count());
            }

            var result = new { SPP = totalspp, KS = totalks };

            return Json(result, JsonRequestBehavior.AllowGet);
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
                Query = Query.Where(x => x.Fullname.ToLower().Contains(Convert.ToString(Fullname.ToString().ToLower())));

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
                var uname = User.Identity.GetUserName();
                List<User> u = db.Users.Where(x => x.Username.Contains(uname)).ToList();
                var isAdmin = false;
                foreach (var uu in u)
                {
                    if (uu.Role_Id == 1)
                    {
                        isAdmin = true;
                    }
                    else
                    {
                        isAdmin = false;
                    }
                }
                foreach (var data in PagedQuery)
                {

                    i++;
                    int Year = Convert.ToInt32(data.Year);
                    var Periode = Year.ToString() + "-" + (Year + 1);
                    listResult.Add(new string[]
                    {
                        i.ToString(),
                        data.Nosisda,
                        data.Fullname,
                        Periode,
                        data.Kelas + "-" + data.Tingkat,
                        isAdmin.ToString(),
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

        public ActionResult TransaksiKosong(string nosisda)
        {
            return View();
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
                dttrans = db.Transaksis.Where(x => x.Nosisda.Equals(nosisda) && x.isCanceled == false);
            }

            /*
             * iMa : if dttrans doest have any record, it will be null, and null cannot use Count!
             */
            if (dttrans == null || dttrans.Count() == 0)
            {
                return RedirectToAction("TransaksiKosong");
            }
            else
            {
                Transaksi tr = dttrans.OrderByDescending(x => x.TransId).First();
                if (tr == null)
                {
                    return RedirectToAction("TransaksiKosong");
                }
                else
                {

                    if (tr.tipebayar != "Tunai")
                    {
                        Bank infobank = db.Banks.Find(tr.BankId);
                        tr.Banknm = infobank.Bankname;
                    }
                }
            }

            return View(dttrans);
        }

        //GET : Transaction/Transaction/Delete
        public ActionResult Delete(string nosisda)
        {
            if (nosisda == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IEnumerable<Transaksi> dttrans = null;
            if (db.Transaksis.Count() != 0)
            {
                dttrans = db.Transaksis.Where(x => x.Nosisda.Equals(nosisda) && x.isCanceled == false);
            }

            /*
             * iMa : if dttrans doest have any record, it will be null, and null cannot use Count!
             */
            if (dttrans == null || dttrans.Count() == 0)
            {
                return RedirectToAction("TransaksiKosong");
            }
            else
            {
                Transaksi tr = dttrans.OrderByDescending(x => x.TransId).First();
                if (tr == null)
                {
                    return RedirectToAction("TransaksiKosong");
                }
                else
                {

                    if (tr.tipebayar != "Tunai")
                    {
                        Bank infobank = db.Banks.Find(tr.BankId);
                        tr.Banknm = infobank.Bankname;
                    }
                }
            }

            return View(dttrans);
        }

        //GET : Transaction/Transaction/Lakukan Transaksi
        public ActionResult FormTrans(TransactionFormCreateVM mod)
        {
            List<SelectListItem> OpTrans = new List<SelectListItem>()

            {
                new SelectListItem {Text="Pilih Cara Transaksi",Value="0",Selected=true },
                new SelectListItem {Text="Tunai",Value="Tunai" },
                new SelectListItem {Text="Transfer",Value="Transfer"},
            };

            List<SelectListItem> listperiode = new List<SelectListItem>()

            {
                new SelectListItem {Text="Pilih periode",Value="0", Selected=true },
                new SelectListItem {Text="2015-2016",Value="2015-2016" },
                new SelectListItem {Text="2016-2017",Value="2016-2017" },
                new SelectListItem {Text="2017-2018",Value="2017-2018" },
                new SelectListItem {Text="2018-2019",Value="2018-2017" },
                new SelectListItem {Text="2019-2020",Value="2019-2020" }
            };

            //list bulan yang sudah dibayarkan
            /*IEnumerable<Transaksi> spp = db.Transaksis.Where(x => x.Nosisda.Equals(mod.Nosisda));
            string[] vv = new string[spp.Count()];
            var bln = "";
            for (int i = 0; i < spp.Count(); i++)
            {
                if (spp.ToList()[i].bulanspp != "-")
                {
                    vv[i] = spp.ToList()[i].bulanspp;
                    //break;
                }
            }
            if (vv != null)
            {
                bln = String.Join(",", vv);
            }

            var bulanBayar = bln.Split(',');
            List<string> blnBayar = new List<string>(bulanBayar);

            //list bulanspp for multiselect
            mod.Categories = db.Bulans.Select(s => new TransactionFormCreateVM { Id = s.BulanId, blnSPP = s.namaBulan }).Where(s => !blnBayar.Contains(s.blnSPP)).ToList();*/

            //list bulan for multiselect
            mod.BulanAJCA = db.Bulans.Select(s => new TransactionFormCreateVM { BulanId = s.BulanId, namaBulan = s.namaBulan }).ToList();

            //list SS for multiselect
            mod.SS = db.SchoolSupports.Select(s => new TransactionFormCreateVM { SSId = s.SsId, JenisSS = s.JenisSS }).ToList();

            //info siswa
            IEnumerable<Siswa> dts = db.Siswas.Where(x => x.Nosisda.Equals(mod.Nosisda));
            string[] keltingkat = null;
            string tkt = "";
            string disc = "";
            string discspp = "";
            string namatingkat = "";
            int diskonbm = 0;
            int diskonspp = 0;
            foreach (var d in dts)
            {
                mod.Namasiswa = d.Fullname;
                mod.periode = d.PerDaftar;
                mod.Kelastingkat = d.Kelas;
                mod.Tingkat = d.Tingkat;
                if (d.Kelas != null || d.Kelas != "")
                {
                    keltingkat = d.Kelas.Split(' ');
                    tkt = keltingkat[0];
                }
                disc = d.TypeDiscAdm;
                discspp = d.TypeDisc;
                namatingkat = d.Tingkat;
                diskonbm = Convert.ToInt32(d.NomDiscAdm);
                diskonspp = Convert.ToInt32(d.NomDisc);

            }

            //info tingkat to get info biaya
            IEnumerable<Tingkat> dtTingkat = db.Tingkats.Where(x => x.Namatingkat.Equals(namatingkat));
            int idtingkat = 0;
            foreach (var t in dtTingkat)
            {
                idtingkat = t.TingkatId;
            }

            //info biaya
            IEnumerable<Biaya> dtb = db.Biayas;
            foreach (var dd in dtb)
            {
                if (dd.TingkatId == idtingkat)
                {

                    if (dd.KatBiaya == "Biaya Masuk")
                    {

                        if (disc == "Rp")
                        {
                            mod.totalBM = Convert.ToString(Convert.ToInt32(dd.NomBiaya) - (diskonbm));
                        }
                        if (disc == "%")
                        {
                            mod.totalBM = Convert.ToString(Convert.ToInt32(dd.NomBiaya) - (Convert.ToInt32(dd.NomBiaya) * (diskonbm) / 100));
                        }
                        else
                        {
                            mod.totalBM = dd.NomBiaya;
                        }
                    }

                    //iMa
                    /*
                     * Separation between SPP and KS due to another field in form
                     */
                    /*if (dd.JenisBiaya == "SPP")
                    {
                        int totalSPP = Convert.ToInt32(mod.bayarspp) + Convert.ToInt32(dd.NomBiaya);
                        mod.bayarspp = totalSPP.ToString();

                        if (discspp == "Rp")
                        {
                            mod.bayarspp = Convert.ToString(Convert.ToInt32(dd.NomBiaya) - (diskonspp));
                        }
                        if (discspp == "%")
                        {
                            mod.bayarspp = Convert.ToString(Convert.ToInt32(dd.NomBiaya) - (Convert.ToInt32(dd.NomBiaya) * (diskonspp) / 100));
                        }

                    }*/

                    /*if (dd.JenisBiaya == "KS")
                    {
                        mod.komiteSekolah = dd.NomBiaya;
                    }*/


                    //if (dd.KatBiaya == "School Support")
                    //{
                    //    mod.nominal = dd.NomBiaya;
                    //}


                    if (dd.KatBiaya == "Daftar Ulang")
                    {
                        mod.daftarUlang = dd.NomBiaya;
                    }
                }

            }

            //info paid BM (BM yang sudah dibayarkan/cicilan BM)
            IEnumerable<Transaksi> dtts = db.Transaksis.Where(x => x.Nosisda.Equals(mod.Nosisda) && x.isCanceled == false);
            foreach (var t in dtts)
            {
                mod.paidBM = Convert.ToString(Convert.ToInt32(mod.paidBM) + Convert.ToInt32(t.bayarBM));
                mod.cicilDaftarUlang = Convert.ToString(Convert.ToInt32(mod.cicilDaftarUlang) + Convert.ToInt32(t.cicilDaftarUlang));
                mod.sisaTagihanBM = Convert.ToString(Convert.ToInt32(t.totalBM) - Convert.ToInt32(mod.paidBM));
                //mod.cicilDaftarUlang = Convert.ToString(Convert.ToInt32(mod.cicilDaftarUlang) + Convert.ToInt32(t.cicilDaftarUlang));
            }

            //info sisa tagihan BM & Daftar Ulang
            IEnumerable<Transaksi> sisa = db.Transaksis.Where(x => x.Nosisda.Equals(mod.Nosisda));
            foreach (var t in sisa)
            {
                //mod.sisaTagihanBM = Convert.ToString(Convert.ToInt32(t.totalBM) - Convert.ToInt32(t.bayarBM));
                mod.sisaTagihanDU = Convert.ToString(Convert.ToInt32(t.daftarUlang) - Convert.ToInt32(t.cicilDaftarUlang));
            }

            var idTingkatCounter = 0;
            if (namatingkat == "PG")
            {
                IEnumerable<Tingkat> t = db.Tingkats.Where(x => x.Namatingkat.Equals("TK A"));
                for (int i = 0; i < t.Count(); i++)
                {
                    idTingkatCounter = t.ToList()[i].TingkatId;
                    break;
                }
                IEnumerable<Biaya> b = db.Biayas.Where(x => x.TingkatId == idTingkatCounter);
                foreach (var m in b)
                {
                    if (m.KatBiaya == "Daftar Ulang")
                    {
                        mod.daftarUlang = m.NomBiaya;
                        break;
                    }
                }
            }
            if (namatingkat == "TK A")
            {
                IEnumerable<Tingkat> t = db.Tingkats.Where(x => x.Namatingkat.Equals("TK B"));
                for (int i = 0; i < t.Count(); i++)
                {
                    idTingkatCounter = t.ToList()[i].TingkatId;
                    break;
                }
                IEnumerable<Biaya> b = db.Biayas.Where(x => x.TingkatId == idTingkatCounter);
                foreach (var m in b)
                {
                    if (m.KatBiaya == "Daftar Ulang")
                    {
                        mod.daftarUlang = m.NomBiaya;
                        break;
                    }
                }
                IEnumerable<Transaksi> totalsemua = db.Transaksis.Where(x => x.bayarspp == idtingkat);
                foreach (var semuatotal in totalsemua)
                {
                    if (semuatotal.totalkeseluruhan == "total Semua")
                    {
                        mod.bayarspp = semuatotal.nominal;
                    }
                }
            }

            ViewBag.OpTrans = OpTrans;
            ViewBag.listperiode = listperiode;
            return View(mod);
        }

        //POST : Transaction/Transaction/Lakukan Transaksi
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FormTrans(TransactionFormCreateVM model, string status)
        {
            //SchoolSupport ss = db.SchoolSupports.Find(Convert.ToInt32(model.getSS.ToList()));
            var spp = ""; //bulanspp
            var ca = ""; //bulan catering
            var aj = ""; //bulan antarjemput
            var ss = "";
            if (model.getBulan == null) spp = "-"; else spp = String.Join(",", model.getBulan);
            if (model.bulanCA == null) ca = "-"; else ca = String.Join(",", model.bulanCA);
            if (model.bulanAJ == null) aj = "-"; else aj = String.Join(",", model.bulanAJ);
            if (model.getSS == null) ss = "-"; else ss = String.Join(",", model.getSS);
            if (ModelState.IsValid)
            {
                Transaksi newmodel = new Transaksi();
                newmodel.Nosisda = model.Nosisda;

                IEnumerable<Siswa> siswa = db.Siswas.Where(s => s.Nosisda.Equals(newmodel.Nosisda));
                var jId = 0;
                var tId = 0;
                foreach (var sis in siswa)
                {
                    tId = Convert.ToInt32(sis.TingkatId);
                    break;
                }
                Tingkat tinfo = db.Tingkats.Find(tId);
                jId = Convert.ToInt32(tinfo.JenjangId);

                Jenjang jinfo = db.Jenjangs.Find(jId);
                

                newmodel.Jenjang = jinfo.JenjangName;
                newmodel.Namasiswa = model.Namasiswa;
                newmodel.Kelastingkat = model.Kelastingkat;
                newmodel.bayarspp = Convert.ToInt32(model.bayarspp);
                newmodel.komiteSekolah = model.komiteSekolah;
                newmodel.totalBM = model.totalBM;
                newmodel.bayarBM = Convert.ToInt32(model.bayarBM);
                newmodel.tipebayar = model.tipebayar;
                newmodel.BankId = model.BankId;
                newmodel.Nokwitansi = model.Nokwitansi;
                //newmodel.Username = userinfo.Username;
                if (model.tipebayar == "Tunai")
                {
                    newmodel.tglbayar = DateTime.UtcNow.Date;
                }
                else
                {
                    newmodel.tgltransfer = Convert.ToDateTime(model.tgltransfer);
                    newmodel.tglbayar = Convert.ToDateTime(model.tgltransfer);
                }
                //newmodel.bayarspp = Convert.ToInt32(model.bayarspp);
                /*newmodel.bulanspp = model.getBulan;*/
                newmodel.bulanspp = spp;
                newmodel.bulanCA = ca;
                newmodel.bulanAJ = aj;
                newmodel.periode = model.periode;
                newmodel.JenisSS = model.JenisSS;
                newmodel.uang = model.uang;
                if(model.uang == "0" || model.uang == null)
                {
                    model.uang = model.total;
                    newmodel.total = Convert.ToString(Convert.ToInt32(model.uang) - Convert.ToInt32(model.total));
                } else
                {
                    newmodel.total = Convert.ToString(Convert.ToInt32(model.uang) - Convert.ToInt32(model.total));
                }

                newmodel.nominal = model.nominal;
                if (model.Kelastingkat == "TK A" || model.Kelastingkat == "PG")
                {
                    newmodel.daftarUlang = model.daftarUlang;
                    newmodel.cicilDaftarUlang = Convert.ToInt32(model.bayarDaftarUlang);
                }

                
                db.Transaksis.Add(newmodel);
                db.SaveChanges();
                int lastid = db.Transaksis.Max(x => x.TransId);
                var test = ss;

                return RedirectToAction("Kwitansi", new { id = lastid, ssid = test });
            }

            return View(model);
        }

        //Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Transaksi model, int? id)
        {
            Transaksi mod = null;
            if (ModelState.IsValid)
            {
                mod = db.Transaksis.Find(model.TransId);

                mod.isCanceled = true;
                mod.canceledBy = HttpContext.User.Identity.Name;
                mod.canceledDate = DateTime.UtcNow.Date;

                db.Entry(mod).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index", System.Web.HttpContext.Current.Session["NamaSiswa"]);
            }

            return View(mod);
        }

        //GET Kwitansi
        public ActionResult Kwitansi(int? id, string ssid, KwitansiFormVM byr)
        {
            string bayarspp = byr.bayarspp;
            string nominal = byr.nominal;
            var uname = User.Identity.GetUserName();
            //var ss = ssid.Split(',');

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //SchoolSupport
            Transaksi transaksi = db.Transaksis.Find(id);
            if (transaksi.SSId != null)
            {
                //string[] jss = new string[ss.Count()];
                //for (int i = 0; i < ss.Count(); i++)
                //{
                //    SchoolSupport s = db.SchoolSupports.Find(Convert.ToInt32(ss[i]));
                //    jss[i] = s.JenisSS;
                //}
                //var tss = String.Join(",", jss);
                //transaksi.JenisSS = tss;
                //info bayar keseluruhan
                Int32 totalbayar = (transaksi.bayarBM == null ? 0 : Convert.ToInt32(transaksi.bayarBM)) + (transaksi.bulanspp == null ? 0 : Convert.ToInt32(transaksi.bayarspp)) + (transaksi.bulanspp == null ? 0 : Convert.ToInt32(transaksi.komiteSekolah)) + (transaksi.nominal == null ? 0 : Convert.ToInt32
                    (transaksi.nominal)) + (transaksi.cicilDaftarUlang == null ? 0 : Convert.ToInt32(transaksi.cicilDaftarUlang));
                transaksi.totalkeseluruhan = totalbayar.ToString();
            }
            else
            {
                //transaksi.JenisSS = "-";

                Int32 totalbayar = (transaksi.bayarBM == null ? 0 : Convert.ToInt32(transaksi.bayarBM)) + (transaksi.bulanspp == null ? 0 : Convert.ToInt32(transaksi.bayarspp)) + (transaksi.bulanspp == null ? 0 : Convert.ToInt32(transaksi.komiteSekolah)) + (transaksi.nominal == null ? 0 : Convert.ToInt32
                    (transaksi.nominal)) + (transaksi.cicilDaftarUlang == null ? 0 : Convert.ToInt32(transaksi.cicilDaftarUlang));
                transaksi.totalkeseluruhan = totalbayar.ToString();
            }

            //spp
            if (transaksi.bulanspp == "-")
            {
                transaksi.infospp = "-";
            }
            if (transaksi.komiteSekolah == null)
            {
                transaksi.infospp = "-";
            }

            //string no kwitansi
            string nosisda = transaksi.Nosisda;
            string randomnosisda = frandom(nosisda);
            string randomalfanum = frandom("");
            string time = DateTime.Now.ToString("HHmmss");
            transaksi.Nokwitansi = randomnosisda + "-" + time + "-" + randomalfanum;

            Transaksi kwitansiupdate = db.Transaksis.Find(transaksi.TransId);
            kwitansiupdate.Nokwitansi = transaksi.Nokwitansi;

            db.Entry(kwitansiupdate).State = EntityState.Modified;
            db.SaveChanges();

            //info kasir
            transaksi.Username = uname;

            //infosiswa
            IEnumerable<Siswa> siswas = db.Siswas.Where(x => x.Nosisda.Equals(transaksi.Nosisda));
            for (int i = 0; i < siswas.Count(); i++)
            {
                transaksi.Namasiswa = siswas.ToList()[i].Fullname;
                break;
            }

            if (transaksi == null)
            {
                return HttpNotFound();
            }

            return View(transaksi);
        }

        //nomor kwitansi
        private static Random random = new Random();
        public static string frandom(string input)
        {
            string result = "";
            if (input == "" || input == null)
            {
                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                //get 5 char from alfanumeric
                result = new string(Enumerable.Repeat(chars, chars.Length).Select(s => s[random.Next(s.Length)]).ToArray());
                result = result.Substring(0, 5);
            }
            else
            {
                //random nosisda
                result = new string(Enumerable.Repeat(input, input.Length).Select(s => s[random.Next(s.Length)]).ToArray());
            }
            return result;
        }
    }
}
