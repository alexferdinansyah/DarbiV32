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
        public ActionResult AjaxSiswa(JQueryDataTableParamModel param)
        {
            var QS = Request.QueryString;
            String Nosisda = QS["Nosisda"];
            Boolean IsActive = (QS["IsActive"] == "false" ? false : true);

            List<string[]> listResult = new List<string[]>();
            String errorMessage = "";

            try
            {
                IEnumerable<Siswa> Query = db.Siswas;
                if (Nosisda != "")
                {
                    Query = Query.Where(x => x.Nosisda.Contains(Nosisda));
                }

                Query = Query.Where(x => x.IsActive == IsActive);

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
            return View(siswa);
        }

        // POST: MasterData/Siswa/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = 
            "SiswaId,Nosisda,Nisn,Fullname,Nickname,Sex,Pob,Dob,NamaAyah,NamaIbu,PekerjaanAyah,PekerjaanIbu,NoTelpAyah,NoTelpIbu,EmailOrtu,Alamat,Kota,Provinsi,KodePos,Negara,Anakke,Agama,Suku,Kewarganegaraan,TinggiBadan,BeratBadan,Goldar,Periode,Kelas,StatKat,KontakSiswa,SekolahAsal,StatSekolahAsal,JarakRumahSekolah,Tgldaftar,Geltest")]
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
                SiswaCek.Periode = siswa.Periode;
                SiswaCek.Kelas = siswa.Kelas;
                SiswaCek.StatKat = siswa.StatKat;
                SiswaCek.KontakSiswa = siswa.KontakSiswa;
                SiswaCek.SekolahAsal = siswa.SekolahAsal;
                SiswaCek.StatSekolahAsal = siswa.StatSekolahAsal;
                SiswaCek.JarakRumahSekolah = siswa.JarakRumahSekolah;
                SiswaCek.Tgldaftar = siswa.Tgldaftar;
                SiswaCek.GelTest = siswa.GelTest;

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