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
using App.Web.Areas.MasterData.Models;
using App.Entities;

namespace App.Web.Areas.MasterData.Controllers
{
    public class SiswaController : Controller
    {
        // GET: MasterData/Siswa
        private DatabaseContext db = new DatabaseContext();

        public ActionResult Index(SiswaSearchFormVM model = null)
        {
            if (model == null)
            {
                model = new SiswaSearchFormVM();
                model.IsActive = true;
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult AjaxSiswa(JQueryDataTableParamModel param, SiswaSearchFormVM m)
        {
            var QS = Request.QueryString;
            string Nosisda = QS["Nosisda"];
            string Fullname = QS["Fullname"];
            //Boolean IsActive = (QS["IsActive"] == "false" ? false : true);

            List<string[]> listResult = new List<string[]>();
            String errorMessage = "";

            try
            {
                IEnumerable<Siswa> Query = db.Siswas;
                if ((Nosisda != "") && (Fullname != ""))
                {
                    Query = Query.Where (x => x.Nosisda.Contains(Nosisda) && x.Fullname.Contains(Fullname));

                } 


                //Query = Query.Where(x => x.IsActive == IsActive);

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
                        //(data.IsActive == true ? "<input type=\"checkbox\" disabled checked>" : "<input type=\"checkbox\" disabled>"),
                        data.SiswaId.ToString()
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

        // GET: MasterData/Siswa/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Siswa siswa = db.Siswas.Find(id);
            if (siswa == null)
            {
                return HttpNotFound();
            }
            return View(siswa);
        }

        // GET: MasterData/Siswa/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Siswa siswa = db.Siswas.Find(id);
            if (siswa == null)
            {
                return HttpNotFound();
            }


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
                    Selected = (i == siswa.Year ? true : false)
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

            
            ViewBag.Years = Years;
            ViewBag.MonthItem = ObjDaftar;
            return View(siswa);
        }

        public ActionResult DetailSaudara(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetailSaudara detailsaudara = db.DetailSaudaras.Find(id);
            if (detailsaudara == null)
            {
                detailsaudara = new DetailSaudara();
                detailsaudara.SiswaId = Convert.ToInt32(id);
            }

            return View(detailsaudara);
        }

        public ActionResult CreateSaudara(int? id)
        {
            DetailSaudara detailsaudara = new DetailSaudara();

            if (id != null)
            {
                detailsaudara.SiswaId = Convert.ToInt32(id);
            }

            List<SelectListItem> ListItem = new List<SelectListItem>()
            {
                new SelectListItem {Text="Pilih Jenis Kelamin",Value="0",Selected=true},
                new SelectListItem {Text="Laki-Laki",Value="1"},
                new SelectListItem {Text="Perempuan",Value="2"},
            };

            ViewBag.ListItem = ListItem;
            return View(detailsaudara);
        }

        //POST : MasterData/DetailSaudara/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateSaudara(DetailSaudara detailsaudara)
        {
            if (ModelState.IsValid)
            {
                DetailSaudara newdetailsaudara = new DetailSaudara();
                newdetailsaudara.Fullname = detailsaudara.Fullname;
                newdetailsaudara.Sex = detailsaudara.Sex;
                newdetailsaudara.Dob = detailsaudara.Dob;
                db.DetailSaudaras.Add(detailsaudara);
                try
                {
                    db.SaveChanges();
                }
                catch (Exception e)
                {

                }
                return RedirectToAction("DetailSaudara", new { id = detailsaudara.DetailSaudaraId });
            }

            return View(detailsaudara);
        }

        [HttpGet]
        public ActionResult AjaxDS(JQueryDataTableParamModel param, int? id)
        {
            var QS = Request.QueryString;
            string val = Convert.ToString(Request.Params["SiswaId"]);
            int SiswaId = Convert.ToInt32(QS["SiswaId"]);
            //String Namatingkat = QS["Namatingkat"];

            //Boolean IsActive = (QS["IsActive"] == "false" ? false : true);

            List<string[]> listResult = new List<string[]>();
            String errorMessage = "";
            if (SiswaId == null)
            {
                //var ds = db.DetailSaudaras.Find()
                return Json(new
                {
                    sEcho = param.sEcho,
                    iTotalRecords = 0,
                    iTotalDisplayRecords = 0,
                    aaData = listResult
                },
                JsonRequestBehavior.AllowGet);
            }
            else
            {
                try
                {
                    IEnumerable<DetailSaudara> Query = db.DetailSaudaras.Where(x => x.SiswaId.Equals(SiswaId));


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
                        data.Fullname,
                        data.Sex,
                        data.Dob,
                        data.DetailSaudaraId.ToString()
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

        // POST: MasterData/Siswa/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SiswaId,Nosisda,Nisn,Fullname,Nickname,Sex,Pob,Dob,NamaAyah,NamaIbu,PekerjaanAyah,PekerjaanIbu," +
            "NoTelpAyah,NoTelpIbu,EmailOrtu,Alamat,Kota,Provinsi,KodePos,Negara,Anakke,Agama,Suku,Kewarganegaraan,TinggiBadan," +
            "BeratBadan,Goldar,PerDaftar,Year,Kelas,Katspp,KontakSiswa,SekolahAsal,StatSekolahAsal,JarakRumahSekolah,TglDaftar")]
         Siswa siswa)
        {
            siswa.DetailSaudara = "";
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                Siswa SiswaCek = db.Siswas.Find(siswa.SiswaId);
                SiswaCek.Fullname = siswa.Fullname;
                SiswaCek.Nickname = siswa.Nickname;
                SiswaCek.Sex = siswa.Sex;
                SiswaCek.Pob = siswa.Pob;
                SiswaCek.Dob = siswa.Dob;
                SiswaCek.NamaAyah = siswa.NamaAyah;
                SiswaCek.NamaIbu = siswa.NamaIbu;
                SiswaCek.PekerjaanAyah = siswa.PekerjaanAyah;
                SiswaCek.PekerjaanIbu = siswa.PekerjaanIbu;
                SiswaCek.NoTelpAyah = siswa.NoTelpAyah;
                SiswaCek.NoTelpIbu = siswa.NoTelpIbu;
                SiswaCek.EmailOrtu = siswa.EmailOrtu;
                SiswaCek.Alamat = siswa.Alamat;
                SiswaCek.Kota = siswa.Kota;
                SiswaCek.Provinsi = siswa.Provinsi;
                SiswaCek.KodePos = siswa.KodePos;
                SiswaCek.Negara = siswa.Negara;
                SiswaCek.Anakke = siswa.Anakke;
                SiswaCek.Agama = siswa.Agama;
                SiswaCek.Suku = siswa.Suku;
                SiswaCek.Kewarganegaraan = siswa.Kewarganegaraan;
                SiswaCek.TinggiBadan = siswa.TinggiBadan;
                SiswaCek.BeratBadan = siswa.BeratBadan;
                SiswaCek.Goldar = siswa.Goldar;
                SiswaCek.PerDaftar = siswa.PerDaftar;
                SiswaCek.Year = siswa.Year;
                SiswaCek.Kelas = siswa.Kelas;
                SiswaCek.KatSpp = siswa.KatSpp;
                SiswaCek.KontakSiswa = siswa.KontakSiswa;
                SiswaCek.SekolahAsal = siswa.SekolahAsal;
                SiswaCek.StatSekolahAsal = siswa.StatSekolahAsal;
                SiswaCek.JarakRumahSekolah = siswa.JarakRumahSekolah;
                SiswaCek.TglDaftar = DateTime.UtcNow.Date;

                db.Entry(SiswaCek).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(siswa);
        }

        // GET: MasterData/Siswa/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Siswa siswa = db.Siswas.Find(id);
            if (siswa == null)
            {
                return HttpNotFound();
            }
            return View(siswa);
        }

        // POST: MasterData/Siswa/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Siswa siswa = db.Siswas.Find(id);
            db.Siswas.Remove(siswa);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: MasterData/DetailSaudara/Delete/5
        public ActionResult DeleteSaudara(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
            }
            DetailSaudara detailsaudara = db.DetailSaudaras.Find(id);
            if (detailsaudara == null)
            {
                return HttpNotFound();
            }
            return View(detailsaudara);
        }

        // POST: MasterData/DetailSaudara/Delete/5
        [HttpPost, ActionName("DeleteSaudara")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmedSaudara(int id)
        {
            DetailSaudara detailsaudara = db.DetailSaudaras.Find(id);
            db.DetailSaudaras.Remove(detailsaudara);
            db.SaveChanges();
            return RedirectToAction("DetailSaudara", new { id = detailsaudara.SiswaId });
        }

        // GET: MasterData/DetailSaudara/Details/5
        public ActionResult DetailsSaudara(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetailSaudara detailsaudara = db.DetailSaudaras.Find(id);
            if (detailsaudara == null)
            {
                return HttpNotFound();
            }
            return View(detailsaudara);
        }

        // GET: MasterData/DetailSaudara/Edit/5
        public ActionResult EditSaudara(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetailSaudara detailsaudara = db.DetailSaudaras.Find(id);
            if (detailsaudara == null)
            {
                return HttpNotFound();
            }
            return View(detailsaudara);
        }

        // POST: MasterData/DetailSaudara/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditSaudara([Bind(Include = "DetailSaudaraId, SiswaId,Fullname,Sex,Dob")]
         DetailSaudara detailsaudara)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                DetailSaudara DetailSaudaraCek = db.DetailSaudaras.Find(detailsaudara.DetailSaudaraId);
                DetailSaudaraCek.Fullname = detailsaudara.Fullname;
                DetailSaudaraCek.Sex = detailsaudara.Sex;
                DetailSaudaraCek.Dob = detailsaudara.Dob;

                db.Entry(DetailSaudaraCek).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DetailSaudara", new { id = DetailSaudaraCek.SiswaId });
                //return RedirectToAction("DetailSaudara", "Siswa", new { id = detailsaudara.SiswaId });
            }
            return View(detailsaudara);
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