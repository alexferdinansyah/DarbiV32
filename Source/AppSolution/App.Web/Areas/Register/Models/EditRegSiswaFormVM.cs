using App.Entities.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Web.Areas.Register.Models
{
    public class EditRegSiswaFormVM : Controller
    {
        // GET: Register/EditRegSiswaFormVM
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Nama Lengkap Siswa")]
        public string NamaSiswa { get; set; }

        [Display(Name = "Nama Ayah Siswa")]
        public string NamaAyah { get; set; }

        [Display(Name = "Nama Ibu Siswa")]
        public string NamaIbu { get; set; }

        [Display(Name = "Kontak Ayah Siswa")]
        public string KontakAyah { get; set; }

        [Display(Name = "Kontak Ibu Siswa")]
        public string KontakIbu { get; set; }

        [Display(Name = "Asal Sekolah")]
        public string AsalSekolah { get; set; }

        [Display(Name = "Kategori Spp")]
        public string KatSpp { get; set; }

        [Display(Name = "Tingkat")]
        public int? TingkatId { get; set; }

        public string Tingkat { get; set; }

        [Display(Name = "Periode Daftar")]
        public string PerDaftar { get; set; }

        [Display(Name = "Gelombang Test")]
        public string GelTest { get; set; }

        [Display(Name = "Tahap 1")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Tahapsatu { get; set; }

        [Display(Name = "Tahap 2")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Tahapdua { get; set; }

        [Display(Name = "Kategori Administrasi")]
        public string KatAdm { get; set; }

        [Display(Name = "Tanggal Daftar")]
        [DataType(DataType.Date)]
        public DateTime TglDaftar { get; set; }

        public SelectList Tingkats()
        {
            DatabaseContext db = new DatabaseContext();
            var Tingkats = db.Tingkats;

            return new SelectList(Tingkats.ToList(), "TingkatId", "Namatingkat", "0");
        }
    }
}