using App.Entities.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Web.Areas.Register.Models
{
    public class CreateRegSiswa
    {
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

        [Display(Name = "Tipe Discount")]
        public string TypeDisc { get; set; }

        [Display(Name = "Nominal Discount")]
        public string NomDisc { get; set; }

        [Display(Name = "Tingkat")]
        public int? TingkatId { get; set; }

        public string Tingkat { get; set; }

        [Display(Name = "Periode Daftar")]
        public string PerDaftar { get; set; }

        [Display(Name = "Tahun")]
        public string Year { get; set; }

        [Display(Name = "Tahap Pembayaran 1")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Tahapsatu { get; set; }

        [Display(Name = "Tahap Pembayaran 2")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Tahapdua { get; set; }

        [Display(Name = "Kategori Biaya Masuk")]
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