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
            SchoolSupport ss =db.SchoolSupports.Find(Convert.ToInt32(inputarray[0]));
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
                dttrans = db.Transaksis.Where(x => x.Nosisda.Equals(nosisda));
            }

            if(dttrans.Count() == 0)
            {
                return HttpNotFound();
            }
            else
            {
                Transaksi tr = dttrans.OrderByDescending(x => x.TransId).First();
                if (tr == null)
                {
                    return HttpNotFound();
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
                mod.periode = d.Periode;
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
                        mod.totalBM = dd.NomBiaya;
                    }
                    if (dd.KatBiaya == "SPP" || dd.KatBiaya == "KS") 
                    {
                        int totalSPP = Convert.ToInt32(mod.bayarspp) + Convert.ToInt32(dd.NomBiaya);
                        mod.bayarspp = totalSPP.ToString();
                    }

                    if (dd.KatBiaya == "Daftar Ulang")
                    {
                        mod.daftarUlang = dd.NomBiaya;
                    }
                }

            }

            //info paid BM (BM yang sudah dibayarkan/cicilan BM)
            IEnumerable<Transaksi> dtts = db.Transaksis.Where(x => x.Nosisda.Equals(mod.Nosisda));
            foreach (var t in dtts)
            {
                mod.paidBM = Convert.ToString(Convert.ToInt32(mod.paidBM) + Convert.ToInt32(t.bayarBM));
                mod.cicilDaftarUlang = Convert.ToString(Convert.ToInt32(mod.cicilDaftarUlang) + Convert.ToInt32(t.bayarDaftarUlang));
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
                newmodel.totalBM = model.totalBM;
                newmodel.bayarBM = Convert.ToInt32(model.bayarBM);
                newmodel.tipebayar = model.tipebayar;
                newmodel.BankId = model.BankId;
                if (model.tipebayar == "Tunai")
                {
                    newmodel.tglbayar = DateTime.UtcNow.Date;
                }
                else
                {
                    newmodel.tgltransfer = Convert.ToDateTime(model.tgltransfer);
                }
                //newmodel.bayarspp = Convert.ToInt32(model.bayarspp);
                newmodel.bulanspp = model.bulanspp;
                newmodel.SSId = model.SSId;
                newmodel.nominal = model.nominal;
                if (model.Kelastingkat == "TK A" || model.Kelastingkat == "PG")
                {
                    newmodel.daftarUlang = model.daftarUlang;
                    newmodel.bayarDaftarUlang = model.bayarDaftarUlang;
                }
                db.Transaksis.Add(newmodel);
                db.SaveChanges();
                int lastid = db.Transaksis.Max(x => x.TransId);

                return RedirectToAction("Kwitansi", new { id = lastid });
            }

            return View(model);
        }

        //GET Kwitansi 
        public ActionResult Kwitansi(int? id, KwitansiFormVM byr)
        {

            string bayarspp = byr.bayarspp;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //SchoolSupport
            Transaksi transaksi = db.Transaksis.Find(id);
            /*SchoolSupport ss = db.SchoolSupports.Find(transaksi.SSId);
            transaksi.JenisSS = ss.JenisSS;*/
            if (transaksi.SSId != null)
            {
                SchoolSupport ss = db.SchoolSupports.Find(transaksi.SSId);
                transaksi.JenisSS = ss.JenisSS;
            }

            //spp
            if (transaksi.bulanspp == null)
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
    }
}