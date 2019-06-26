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
                    i++;
                    listResult.Add(new string[]
                    {
                        i.ToString(),
                        data.NamaSiswa,
                        data.NamaAyah,
                        data.NamaIbu,
                        data.KontakAyah,
                        data.KontakIbu,
                        data.AsalSekolah,
                        data.KatSpp,
                        data.TingkatId.ToString(),
                        data.PerDaftar,
                        data.GelTest,
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

            List<SelectListItem> ObjItem = new List<SelectListItem>()
            {
                new SelectListItem {Text="Pilih Kategori",Value="0",Selected=true },
                new SelectListItem {Text="UMUM",Value="1" },
                new SelectListItem {Text="Anak Guru",Value="2"},
                new SelectListItem {Text="KHUSUS",Value="3"},
            };

            List<SelectListItem> ObjDaftar = new List<SelectListItem>()
            {
                new SelectListItem {Text="Juli",Value="1"},
                new SelectListItem {Text="Agustus",Value="2" },
                new SelectListItem {Text="September",Value="3"},
                new SelectListItem {Text="Oktober",Value="4"},
                new SelectListItem {Text="November",Value="5"},
                new SelectListItem {Text="Desember",Value="6"},
                new SelectListItem {Text="Januari",Value="7"},
                new SelectListItem {Text="Februari",Value="8"},
                new SelectListItem {Text="Maret",Value="9"},
                new SelectListItem {Text="April",Value="10"},
                new SelectListItem {Text="Mei",Value="11"},
                new SelectListItem {Text="Juni",Value="12"},
            };

            List<SelectListItem> ObjTest = new List<SelectListItem>()
            {
                new SelectListItem {Text="1",Value="1"},
                new SelectListItem {Text="2",Value="2" },
                new SelectListItem {Text="3",Value="3"},
                new SelectListItem {Text="4",Value="4"},
            };

            List<SelectListItem> ObjAdm = new List<SelectListItem>()
            {
                new SelectListItem {Text="Umum",Value="1"},
                new SelectListItem {Text="Bersamaan dengan saudara kandung",Value="2" },
                new SelectListItem {Text="Memiliki saudara kandung",Value="3"},
                new SelectListItem {Text="Umum grade B",Value="4"},
                new SelectListItem {Text="Umum memiliki saudara kandung di SMP + grade B",Value="5"},
                new SelectListItem {Text="Umum bersamaan dengan saudara kandung + grade B",Value="6"},
                new SelectListItem {Text="Asal Darbi + Grade A",Value="7"},
                new SelectListItem {Text="Asal Darbi + Grade B",Value="8"},
                new SelectListItem {Text="Anak Pegawai ke-1",Value="9"},
                new SelectListItem {Text="Anak Pegawai ke-2",Value="10" },
                new SelectListItem {Text="Anak Pegawai ke-3, dst",Value="11"},
                new SelectListItem {Text="Anak Pegawai ke-1 + Grade A",Value="12"},
                new SelectListItem {Text="Anak Pegawai ke-2 + Grade B",Value="13" },
                new SelectListItem {Text="Anak Pegawai ke-3, dst + Grade A",Value="14"},
                new SelectListItem {Text="Anak Pegawai ke-3, dst + Grade B",Value="15"},
                new SelectListItem {Text="Siswa Pindahan ke Toddler semester II",Value="16"},
                new SelectListItem {Text="Siswa pindahan ke PG/TK A/TK B Semester II",Value="17"},
                new SelectListItem {Text="Siswa pindahan ke SD 3-4",Value="18"},
                new SelectListItem {Text="Siswa pindahan ke SD 5-6",Value="19"},
                new SelectListItem {Text="Siswa pindahan ke SMP 8",Value="20"},
                new SelectListItem {Text="Siswa pindahan ke SMP 9",Value="21" },
                new SelectListItem {Text="Daftar ulang siswa TK A, dst",Value="22"},
                new SelectListItem {Text="Daftar ulang siswa TK B",Value="23"},
            };

            ViewBag.KatItem = ObjAdm;
            ViewBag.TestItem = ObjTest;
            ViewBag.MonthItem = ObjDaftar;
            ViewBag.ListItem = ObjItem;
            return View(model);
        }
        //POST : Register/RegSiswa/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateRegSiswa model)
        {
            //var Objd = ViewBag.MonthItem;
            if (ModelState.IsValid)
            {
                RegSiswa newmodel = new RegSiswa();
                newmodel.NamaSiswa = model.NamaSiswa;
                newmodel.NamaAyah = model.NamaAyah;
                newmodel.NamaIbu = model.NamaIbu;
                newmodel.KontakAyah = model.KontakAyah;
                newmodel.KontakIbu = model.KontakIbu;
                newmodel.AsalSekolah = model.AsalSekolah;
                newmodel.KatSpp = model.KatSpp;
                newmodel.TingkatId = model.TingkatId;
                newmodel.PerDaftar = model.PerDaftar;
                newmodel.GelTest = model.GelTest;
                newmodel.Tahapsatu = model.Tahapsatu;
                newmodel.Tahapdua = model.Tahapdua;
                newmodel.KatAdm = model.KatAdm;
                newmodel.TglDaftar = DateTime.UtcNow.Date;

                db.RegSiswas.Add(newmodel);
                db.SaveChanges();
                return RedirectToAction("Index");
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

            model.NamaSiswa = regsiswa.NamaSiswa;
            model.NamaAyah = regsiswa.NamaAyah;
            model.NamaIbu = regsiswa.NamaIbu;
            model.KontakAyah = regsiswa.KontakAyah;
            model.KontakIbu = regsiswa.KontakIbu;
            model.AsalSekolah = regsiswa.AsalSekolah;
            model.KatSpp = regsiswa.KatSpp;
            model.TingkatId = regsiswa.TingkatId;
            model.PerDaftar = regsiswa.PerDaftar;
            model.GelTest = regsiswa.GelTest;
            model.Tahapsatu = regsiswa.Tahapsatu;
            model.Tahapdua = regsiswa.Tahapdua;
            model.KatAdm = regsiswa.KatAdm;
            model.TglDaftar = DateTime.UtcNow.Date;
            List<SelectListItem> ObjItem = new List<SelectListItem>()
            {
                new SelectListItem {Text="Pilih Kategori",Value="0",Selected=true },
                new SelectListItem {Text="UMUM",Value="1" },
                new SelectListItem {Text="Anak Guru",Value="2"},
                new SelectListItem {Text="KHUSUS",Value="3"},
            };

            List<SelectListItem> ObjDaftar = new List<SelectListItem>()
            {
                new SelectListItem {Text="Juli",Value="1"},
                new SelectListItem {Text="Agustus",Value="2" },
                new SelectListItem {Text="September",Value="3"},
                new SelectListItem {Text="Oktober",Value="4"},
                new SelectListItem {Text="November",Value="5"},
                new SelectListItem {Text="Desember",Value="6"},
                new SelectListItem {Text="Januari",Value="7"},
                new SelectListItem {Text="Februari",Value="8"},
                new SelectListItem {Text="Maret",Value="9"},
                new SelectListItem {Text="April",Value="10"},
                new SelectListItem {Text="Mei",Value="11"},
                new SelectListItem {Text="Juni",Value="12"},
            };

            List<SelectListItem> ObjTest = new List<SelectListItem>()
            {
                new SelectListItem {Text="1",Value="1"},
                new SelectListItem {Text="2",Value="2" },
                new SelectListItem {Text="3",Value="3"},
                new SelectListItem {Text="4",Value="4"},
            };

            List<SelectListItem> ObjAdm = new List<SelectListItem>()
            {
                new SelectListItem {Text="Umum",Value="1"},
                new SelectListItem {Text="Bersamaan dengan saudara kandung",Value="2" },
                new SelectListItem {Text="Memiliki saudara kandung",Value="3"},
                new SelectListItem {Text="Umum grade B",Value="4"},
                new SelectListItem {Text="Umum memiliki saudara kandung di SMP + grade B",Value="5"},
                new SelectListItem {Text="Umum bersamaan dengan saudara kandung + grade B",Value="6"},
                new SelectListItem {Text="Asal Darbi + Grade A",Value="7"},
                new SelectListItem {Text="Asal Darbi + Grade B",Value="8"},
                new SelectListItem {Text="Anak Pegawai ke-1",Value="9"},
                new SelectListItem {Text="Anak Pegawai ke-2",Value="10" },
                new SelectListItem {Text="Anak Pegawai ke-3, dst",Value="11"},
                new SelectListItem {Text="Anak Pegawai ke-1 + Grade A",Value="12"},
                new SelectListItem {Text="Anak Pegawai ke-2 + Grade B",Value="13" },
                new SelectListItem {Text="Anak Pegawai ke-3, dst + Grade A",Value="14"},
                new SelectListItem {Text="Anak Pegawai ke-3, dst + Grade B",Value="15"},
                new SelectListItem {Text="Siswa Pindahan ke Toddler semester II",Value="16"},
                new SelectListItem {Text="Siswa pindahan ke PG/TK A/TK B Semester II",Value="17"},
                new SelectListItem {Text="Siswa pindahan ke SD 3-4",Value="18"},
                new SelectListItem {Text="Siswa pindahan ke SD 5-6",Value="19"},
                new SelectListItem {Text="Siswa pindahan ke SMP 8",Value="20"},
                new SelectListItem {Text="Siswa pindahan ke SMP 9",Value="21" },
                new SelectListItem {Text="Daftar ulang siswa TK A, dst",Value="22"},
                new SelectListItem {Text="Daftar ulang siswa TK B",Value="23"},
            };

            ViewBag.KatItem = ObjAdm;
            ViewBag.TestItem = ObjTest;
            ViewBag.MonthItem = ObjDaftar;
            ViewBag.ListItem = ObjItem;
            return View(model);
        }
        // POST: AccessManagement/User/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RegSiswaId,NamaSiswa,NamaAyah,NamaIbu,KontakAyah,KontakIbu," +
            "AsalSekolah,KatSpp,TingkatId,PerDaftar,GelTest,Tahapsatu,Tahapdua,KatAdm,TglDaftar")] RegSiswa regsiswa)
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
                RegCek.TingkatId = regsiswa.TingkatId;
                RegCek.PerDaftar = regsiswa.PerDaftar;
                RegCek.GelTest = regsiswa.GelTest;
                RegCek.Tahapsatu = regsiswa.Tahapsatu;
                RegCek.Tahapdua = regsiswa.Tahapdua;
                RegCek.KatAdm = regsiswa.KatAdm;
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
