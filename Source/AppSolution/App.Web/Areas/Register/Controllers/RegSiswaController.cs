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
using App.Web.Areas.Register.Models;
using App.Entities;

namespace App.Web.Areas.Register.Controllers
{
    public class RegSiswaController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        //GET : Register/RegSiswa
        public ActionResult Index(RegSiswaSearchFormVM model = null)
        {
            if (model == null)
            {
                model = new RegSiswaSearchFormVM();
            }
            return View(model);
        }

        //Get : TableRegSiswa
        [HttpGet]
        public ActionResult AjaxRegSiswa(JQueryDataTableParamModel param)
        {
            var QS = Request.QueryString;
            String NamaSiswa = QS["NamaSiswa"];
            //Boolean IsActive = (QS["IsActive"] == "false" ? false : true);

            List<string[]> listResult = new List<string[]>();
            String errorMessage = "";

            try
            {
                IEnumerable<RegSiswa> Query = db.RegSiswas;
                if (NamaSiswa != "")
                {
                    Query = Query.Where(x => x.NamaSiswa.Contains(NamaSiswa));
                }

                //Query = Query.Where(x => x.IsActive == IsActive);

                int TotalRecord = Query.Count();

                var OrderedQuery = Query.OrderBy(x => x.RegSiswaId);

                int pageSize = param.iDisplayLength;
                int pageNumber = param.iDisplayStart == 0 ? 1 : (param.iDisplayStart / param.iDisplayLength) + 1; ;
                var PagedQuery = OrderedQuery.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();

                int i = 0;
                foreach (var data in PagedQuery)
                {
                    Tingkat infotingkat = db.Tingkats.Find(data.TingkatId);
                    data.Tingkat = infotingkat.Namatingkat;
                    i++;
                    listResult.Add(new string[]
                    {
                        i.ToString(),
                        data.NamaSiswa,
                        data.KatSpp,
                        data.Tingkat,
                        data.PerDaftar,
                        data.Year,
                        data.Tahapsatu.ToString(),
                        data.Tahapdua.ToString(),
                        data.KatAdm,
                        data.TglDaftar.ToString(),
                        //(data.IsActive == true ? "<input type=\"checkbox\" disabled checked>" : "<input type=\"checkbox\" disabled>"),
                        data.RegSiswaId.ToString()
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

        //GET : Register/RegSiswa/Create
        public ActionResult Create()
        {
            CreateRegSiswa model = new CreateRegSiswa();

            List<string> Years = new List<string>();
            DateTime startYear = DateTime.Now;
            Years.Add("Pilih Tahun");
            while (startYear.Year <= DateTime.Now.AddYears(3).Year)
            {
                Years.Add(startYear.Year.ToString());
                startYear = startYear.AddYears(1);
            }

            List<SelectListItem> Disc = new List<SelectListItem>()
            {
                new SelectListItem {Text="Pilih Tipe Diskon",Value="0",Selected=true },
                new SelectListItem {Text="Rp",Value="Rp"},
                new SelectListItem {Text="%",Value="%" },
            };

            List<SelectListItem> DiscAdm = new List<SelectListItem>()
            {
                new SelectListItem {Text="Pilih Tipe Diskon",Value="0",Selected=true },
                new SelectListItem {Text="Rp",Value="Rp"},
                new SelectListItem {Text="%",Value="%" },
            };

            List<SelectListItem> ObjDaftar = new List<SelectListItem>()
            {
                new SelectListItem {Text="Pilih Bulan",Value="0",Selected=true },
                new SelectListItem {Text="Juli",Value="Juli"},
                new SelectListItem {Text="Agustus",Value="Agustus" },
                new SelectListItem {Text="September",Value="September"},
                new SelectListItem {Text="Oktober",Value="Oktober"},
                new SelectListItem {Text="November",Value="November"},
                new SelectListItem {Text="Desember",Value="Desember"},
                new SelectListItem {Text="Januari",Value="Januari"},
                new SelectListItem {Text="Februari",Value="Februari"},
                new SelectListItem {Text="Maret",Value="Maret"},
                new SelectListItem {Text="April",Value="April"},
                new SelectListItem {Text="Mei",Value="Mei"},
                new SelectListItem {Text="Juni",Value="Juni"},
            };
            ViewBag.DiscAdm = DiscAdm;
            ViewBag.Disc = Disc;
            ViewBag.Years = Years;
            ViewBag.MonthItem = ObjDaftar;
            return View(model);
        }
        //POST : Register/RegSiswa/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateRegSiswa model)
        {
            try
            {
                List<string> Years = new List<string>();
                DateTime startYear = DateTime.Now;
                Years.Add("Pilih Tahun");
                while (startYear.Year <= DateTime.Now.AddYears(3).Year)
                {
                    Years.Add(startYear.Year.ToString());
                    startYear = startYear.AddYears(1);
                }
                List<SelectListItem> Disc = new List<SelectListItem>()
            {
                new SelectListItem {Text="Pilih Tipe Diskon",Value="0",Selected=true },
                new SelectListItem {Text="Rp",Value="Rp"},
                new SelectListItem {Text="%",Value="%" },
            };

                List<SelectListItem> DiscAdm = new List<SelectListItem>()
            {
                new SelectListItem {Text="Pilih Tipe Diskon",Value="0",Selected=true },
                new SelectListItem {Text="Rp",Value="Rp"},
                new SelectListItem {Text="%",Value="%" },
            };

                List<SelectListItem> ObjDaftar = new List<SelectListItem>()
            {
                new SelectListItem {Text="Pilih Bulan",Value="0",Selected=true },
                new SelectListItem {Text="Juli",Value="Juli"},
                new SelectListItem {Text="Agustus",Value="Agustus" },
                new SelectListItem {Text="September",Value="September"},
                new SelectListItem {Text="Oktober",Value="Oktober"},
                new SelectListItem {Text="November",Value="November"},
                new SelectListItem {Text="Desember",Value="Desember"},
                new SelectListItem {Text="Januari",Value="Januari"},
                new SelectListItem {Text="Februari",Value="Februari"},
                new SelectListItem {Text="Maret",Value="Maret"},
                new SelectListItem {Text="April",Value="April"},
                new SelectListItem {Text="Mei",Value="Mei"},
                new SelectListItem {Text="Juni",Value="Juni"},
            };
                ViewBag.DiscAdm = DiscAdm;
                ViewBag.Disc = Disc;
                ViewBag.Years = Years;
                ViewBag.MonthItem = ObjDaftar;
                //var Objd = ViewBag.MonthItem;
                if (ModelState.IsValid)
                {
                    Siswa newmodel = new Siswa();
                    newmodel.Fullname = model.Fullname;
                    newmodel.Nickname = model.Nickname;
                    newmodel.Nisn = model.Nisn;
                    newmodel.Sex = model.Sex;
                    newmodel.Pob = model.Pob;
                    newmodel.Dob = model.Dob;
                    newmodel.NamaAyah = model.NamaAyah;
                    newmodel.NamaIbu = model.NamaIbu;
                    newmodel.PekerjaanAyah = model.PekerjaanAyah;
                    newmodel.PekerjaanIbu = model.PekerjaanIbu;
                    newmodel.NoTelpAyah = model.NoTelpAyah;
                    newmodel.NoTelpIbu = model.NoTelpIbu;
                    newmodel.EmailOrtu = model.EmailOrtu;
                    newmodel.Alamat = model.Alamat;
                    newmodel.Kota = model.Kota;
                    newmodel.Provinsi = model.Provinsi;
                    newmodel.KodePos = model.KodePos;
                    newmodel.Negara = model.Negara;
                    newmodel.Anakke = model.Anakke;
                    newmodel.Agama = model.Agama;
                    newmodel.Suku = model.Suku;
                    newmodel.Kewarganegaraan = model.Kewarganegaraan;
                    newmodel.TinggiBadan = model.TinggiBadan;
                    newmodel.BeratBadan = model.BeratBadan;
                    newmodel.KontakSiswa = model.KontakSiswa;
                    newmodel.SekolahAsal = model.SekolahAsal;
                    newmodel.StatSekolahAsal = model.StatSekolahAsal;
                    newmodel.JarakRumahSekolah = model.JarakRumahSekolah;
                    newmodel.KatSpp = model.KatSpp;
                    newmodel.TypeDisc = model.TypeDisc;
                    newmodel.NomDisc = model.NomDisc;
                    newmodel.TingkatId = model.TingkatId;
                    newmodel.PerDaftar = model.PerDaftar;
                    newmodel.Year = model.Year;
                    newmodel.Tahapsatu = model.Tahapsatu;
                    newmodel.Tahapdua = model.Tahapdua;
                    newmodel.KatAdm = model.KatAdm;
                    newmodel.TypeDiscAdm = model.TypeDiscAdm;
                    newmodel.NomDiscAdm = model.NomDiscAdm;
                    newmodel.TglDaftar = DateTime.UtcNow.Date;
                    newmodel.IsActive = true;


                    //nosisda generator
                    Siswa LastSiswa = db.Siswas.OrderByDescending(m => m.SiswaId).First();
                    var lastnosisda = Convert.ToInt32(LastSiswa.Nosisda) + 1;
                    //var newtotalsiswa = totalsiswa.ToString().PadLeft(6, '0');
                    var gnosisda = DateTime.UtcNow.Year;
                    //var query = select distinct(max(SiswaId)) from Siswa;

                    newmodel.Nosisda =  lastnosisda.ToString();
                    
                    db.Siswas.Add(newmodel);
                    try
                    {
                        db.SaveChanges();
                        //getlatestID
                        //save to siswa
                        //return RedirectToAction("~/MasterData/Siswa/Index");
                        return RedirectToAction("Index", "Siswa", new { area = "MasterData" });
                    }
                    catch (Exception e)
                    {

                    }

                }
            }
            catch (Exception e)
            {

            }

            return View(model);
        }

        // GET: Register/RegSiswa/Details
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegSiswa regsiswa = db.RegSiswas.Find(id);
            Tingkat infotingkat = db.Tingkats.Find(regsiswa.TingkatId);
            regsiswa.Tingkat = infotingkat.Namatingkat;
            if (regsiswa == null)
            {
                return HttpNotFound();
            }
            return View(regsiswa);
        }

        // GET: AccessManagement/User/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegSiswa regsiswa = db.RegSiswas.Find(id);
            if (regsiswa == null)
            {
                return HttpNotFound();
            }
            EditRegSiswaFormVM model = new EditRegSiswaFormVM();
            model.RegSiswaId = regsiswa.RegSiswaId;
            model.NamaSiswa = regsiswa.NamaSiswa;
            model.NamaAyah = regsiswa.NamaAyah;
            model.NamaIbu = regsiswa.NamaIbu;
            model.KontakAyah = regsiswa.KontakAyah;
            model.KontakIbu = regsiswa.KontakIbu;
            model.AsalSekolah = regsiswa.AsalSekolah;
            model.KatSpp = regsiswa.KatSpp;
            model.TypeDisc = regsiswa.TypeDisc;
            model.NomDisc = regsiswa.NomDisc;
            model.TingkatId = regsiswa.TingkatId;
            model.PerDaftar = regsiswa.PerDaftar;
            model.Year = regsiswa.Year;
            model.Tahapsatu = regsiswa.Tahapsatu;
            model.Tahapdua = regsiswa.Tahapdua;
            //model.Tahapdua = Convert.ToDateTime();
            model.KatAdm = regsiswa.KatAdm;
            model.TglDaftar = DateTime.UtcNow.Date;

            List<string> Tahunmasuk = new List<string>();

            DateTime startYear = DateTime.Now;
            while (startYear.Year <= DateTime.Now.AddYears(3).Year)
            {
                Tahunmasuk.Add(startYear.Year.ToString());
                startYear = startYear.AddYears(1);
            }
            List<SelectListItem> Years = new List<SelectListItem>()
            {
                new SelectListItem {Text="Pilih Tahun",Value=""}

            };
            foreach (var i in Tahunmasuk)
            {
                Years.Add(new SelectListItem()
                {
                    Text = i,
                    Value = i,
                    Selected = (i == regsiswa.Year ? true : false)
                });
            }

            List<SelectListItem> ObjDaftar = new List<SelectListItem>()
            {
                new SelectListItem {Text="Juli",Value="Juli"},
                new SelectListItem {Text="Agustus",Value="Agustus" },
                new SelectListItem {Text="September",Value="September"},
                new SelectListItem {Text="Oktober",Value="Oktober"},
                new SelectListItem {Text="November",Value="November"},
                new SelectListItem {Text="Desember",Value="Desember"},
                new SelectListItem {Text="Januari",Value="Januari"},
                new SelectListItem {Text="Februari",Value="Februari"},
                new SelectListItem {Text="Maret",Value="Maret"},
                new SelectListItem {Text="April",Value="April"},
                new SelectListItem {Text="Mei",Value="Mei"},
                new SelectListItem {Text="Juni",Value="Juni"},
            };

            List<SelectListItem> Disc = new List<SelectListItem>()
            {
                new SelectListItem {Text="Pilih Tipe Diskon",Value="0",Selected=true },
                new SelectListItem {Text="Rp",Value="Rp"},
                new SelectListItem {Text="%",Value="%" },
            };

            List<SelectListItem> DiscAdm = new List<SelectListItem>()
            {
                new SelectListItem {Text="Pilih Tipe Diskon",Value="0",Selected=true },
                new SelectListItem {Text="Rp",Value="Rp"},
                new SelectListItem {Text="%",Value="%" },
            };

            ViewBag.DiscAdm = DiscAdm;
            ViewBag.Disc = Disc;
            ViewBag.Years = Years;
            ViewBag.MonthItem = ObjDaftar;
            return View(model);
        }
        // POST: AccessManagement/User/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RegSiswaId,NamaSiswa,NamaAyah,NamaIbu,KontakAyah,KontakIbu," +
            "AsalSekolah,KatSpp,TypeDisc,NomDisc,TingkatId,PerDaftar,Year,Tahapsatu,Tahapdua,KatAdm,TypeDiscAdm,NomDiscAdm,TglDaftar")] RegSiswa regsiswa)
        {
            if (ModelState.IsValid)
            {
                RegSiswa RegCek = db.RegSiswas.Find(regsiswa.RegSiswaId);
                RegCek.NamaSiswa = regsiswa.NamaSiswa;
                RegCek.NamaAyah = regsiswa.NamaAyah;
                RegCek.NamaIbu = regsiswa.NamaIbu;
                RegCek.KontakAyah = regsiswa.KontakAyah;
                RegCek.KontakIbu = regsiswa.KontakIbu;
                RegCek.AsalSekolah = regsiswa.AsalSekolah;
                RegCek.KatSpp = regsiswa.KatSpp;
                RegCek.TypeDisc = regsiswa.TypeDisc;
                RegCek.NomDisc = regsiswa.NomDisc;
                RegCek.TingkatId = regsiswa.TingkatId;
                RegCek.PerDaftar = regsiswa.PerDaftar;
                RegCek.Year = regsiswa.Year;
                RegCek.Tahapsatu = regsiswa.Tahapsatu;
                RegCek.Tahapdua = regsiswa.Tahapdua;
                RegCek.KatAdm = regsiswa.KatAdm;
                RegCek.TypeDiscAdm = regsiswa.TypeDiscAdm;
                RegCek.NomDiscAdm = regsiswa.NomDiscAdm;
                RegCek.TglDaftar = DateTime.UtcNow.Date;

                db.Entry(RegCek).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(regsiswa);
        }

        // GET: MasterData/Siswa/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegSiswa regsiswa = db.RegSiswas.Find(id);
            if (regsiswa == null)
            {
                return HttpNotFound();
            }
            return View(regsiswa);
        }

        // POST: MasterData/Siswa/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RegSiswa regsiswa = db.RegSiswas.Find(id);
            db.RegSiswas.Remove(regsiswa);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
