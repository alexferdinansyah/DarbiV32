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


        public ActionResult FirstAjax(string input)
        {
            //2-9 Mekah : idSchoolSupport-kelastingkat
            var inputarray = input.Split('-');
            var kt = inputarray[1].Split(' ');
            string tkt = "";
            tkt = kt[0].ToString();
            SchoolSupport ss = db.SchoolSupports.Find(Convert.ToInt32(inputarray[0]));
            IEnumerable<Tingkat> ts = db.Tingkats.Where(x => x.Namatingkat.Equals(tkt));
            IEnumerable<Biaya> b = null;
            foreach (var d in ts)
            {
                b = db.Biayas.Where(x => x.TingkatId == d.TingkatId);
            }
            b = b.Where(x => x.JenisBiaya.ToLower().Equals(ss.JenisSS.ToLower()) && x.KatBiaya.ToLower().Equals("school support"));
            string nb = "";
            foreach (var d in b)
            {
                nb = d.NomBiaya;
            }
            return Json(nb, JsonRequestBehavior.AllowGet);
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
                dttrans = db.Transaksis.Where(x => x.Nosisda.Equals(nosisda) && x.isCanceled == false) ;
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
            IEnumerable<Transaksi> tran = null;
            Transaksi model = null;
            if (nosisda == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                tran = db.Transaksis.Where(x => x.Nosisda.Equals(nosisda) && x.isCanceled == false).OrderByDescending(x => x.TransId);
            }

            for (int i = 0; i < tran.Count(); i++)
            {
                model = tran.ToList()[i];
                break;
            }

            if (model == null)
            {
                return RedirectToAction("TransaksiKosong");
            }

            return View(model);
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

            List<SelectListItem> listbln = new List<SelectListItem>()

            {
                new SelectListItem {Text="Pilih Bulan",Value="0",Selected=true },
                new SelectListItem {Text="Juli",Value="7" },
                new SelectListItem {Text="Agustus",Value="8" },
            };

            //info siswa
            IEnumerable<Siswa> dts = db.Siswas.Where(x => x.Nosisda.Equals(mod.Nosisda));
            string[] keltingkat = null;
            string tkt = "";
            foreach (var d in dts)
            {
                mod.Namasiswa = d.Fullname;
                mod.periode = d.PerDaftar;
                mod.Kelastingkat = d.Kelas;
                if (d.Kelas != null || d.Kelas != "")
                {
                    keltingkat = d.Kelas.Split(' ');
                    tkt = keltingkat[0];
                }
            }

            //info tingkat to get info biaya
            IEnumerable<Tingkat> dtTingkat = db.Tingkats.Where(x => x.Namatingkat.Equals(tkt));
            int idtingkat = 0;
            var nama = "";
            foreach (var t in dtTingkat)
            {
                idtingkat = t.TingkatId;
                nama = t.Namatingkat;
            }

            //info biaya
            IEnumerable<Biaya> dtb = db.Biayas;
            foreach (var dd in dtb)
            {
                if (dd.TingkatId == idtingkat)
                {
                    if (dd.KatBiaya == "Biaya Masuk")
                    {
                        mod.totalBM = dd.NomBiaya;
                    }

                    //iMa
                    /*
                     * Separation between SPP and KS due to another field in form
                     */
                    if (dd.JenisBiaya == "SPP")
                    //if (dd.KatBiaya == "SPP" || dd.KatBiaya == "KS")
                    {
                        int totalSPP = Convert.ToInt32(mod.bayarspp) + Convert.ToInt32(dd.NomBiaya);
                        mod.bayarspp = totalSPP.ToString();
                    }
                    if(dd.JenisBiaya == "KS")
                    {
                        mod.komiteSekolah = dd.NomBiaya;
                    }

                //if (dd.KatBiaya == "School Support")
                //{
                //    mod.nominal = dd.NomBiaya;
                //}


                if (dd.KatBiaya == "Daftar Ulang")
                {
                    mod.daftarUlang = dd.NomBiaya;

                    /*if (dd.KatBiaya == "Daftar Ulang")
                    {
                        mod.daftarUlang = dd.NomBiaya;
                    }*/

                }
            }

        }

        //info paid BM (BM yang sudah dibayarkan/cicilan BM)
        IEnumerable<Transaksi> dtts = db.Transaksis.Where(x => x.Nosisda.Equals(mod.Nosisda) && x.isCanceled == false);
        foreach (var t in dtts)
        {
            mod.paidBM = Convert.ToString(Convert.ToInt32(mod.paidBM) + Convert.ToInt32(t.bayarBM));
            mod.cicilDaftarUlang = Convert.ToString(Convert.ToInt32(mod.cicilDaftarUlang) + Convert.ToInt32(t.cicilDaftarUlang));
        }

            var idTingkatCounter = 0;
            if (nama == "PG")
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
            if (nama == "TK A")
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
                    if(semuatotal.totalkeseluruhan == "total Semua")
                    {
                        mod.bayarspp = semuatotal.nominal;
                    }
                }
            }

            ViewBag.OpTrans = OpTrans;
            ViewBag.listbln = listbln;
            return View(mod);
        }

        //POST : Transaction/Transaction/Lakukan Transaksi
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FormTrans(TransactionFormCreateVM model, string status)
        {
            if (ModelState.IsValid)
            {
                Transaksi newmodel = new Transaksi();
                newmodel.Nosisda = model.Nosisda;
                newmodel.Namasiswa = model.Namasiswa;
                newmodel.Kelastingkat = model.Kelastingkat;
                newmodel.periode = "2019-2020";
                newmodel.bayarspp = Convert.ToInt32(model.bayarspp);
                newmodel.komiteSekolah = model.komiteSekolah;
                newmodel.totalBM = model.totalBM;
                newmodel.bayarBM = Convert.ToInt32(model.bayarBM);
                newmodel.tipebayar = model.tipebayar;
                newmodel.BankId = model.BankId;
                newmodel.komiteSekolah = model.komiteSekolah;
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
                newmodel.bulanspp = model.bulanspp;
                newmodel.SSId = model.SSId;
                newmodel.nominal = model.nominal;
                if (model.Kelastingkat == "TK A" || model.Kelastingkat == "PG")
                {
                    newmodel.daftarUlang = model.daftarUlang;
                    newmodel.cicilDaftarUlang = Convert.ToInt32(model.bayarDaftarUlang);
                }
                db.Transaksis.Add(newmodel);
                db.SaveChanges();
                int lastid = db.Transaksis.Max(x => x.TransId);

                return RedirectToAction("Kwitansi", new { id = lastid });
            }

            return View(model);
        }

        //GET Kwitansi 
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
        public ActionResult Kwitansi(int? id, KwitansiFormVM byr)
        {

            string bayarspp = byr.bayarspp;
            string nominal = byr.nominal;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //SchoolSupport
            Transaksi transaksi = db.Transaksis.Find(id);
            if (transaksi.SSId != null)
            {
                SchoolSupport s = db.SchoolSupports.Find(transaksi.SSId); 
                transaksi.JenisSS = s.JenisSS;
                //info bayar keseluruhan
                Int32 totalbayar = (transaksi.bayarBM == null ? 0 : Convert.ToInt32(transaksi.bayarBM)) + (transaksi.nominal == null ? 0 : Convert.ToInt32
                    (transaksi.nominal)) + (transaksi.cicilDaftarUlang == null ? 0 : Convert.ToInt32(transaksi.cicilDaftarUlang)) + (transaksi.bulanspp == null ? 0 : Convert.ToInt32(transaksi.komiteSekolah)) + (transaksi.bulanspp == null ? 0 : Convert.ToInt32(transaksi.bayarspp));
                transaksi.totalkeseluruhan = totalbayar.ToString();
            }

            //spp
            if (transaksi.bulanspp == null)
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

        //totalbayar

        
    }
}
