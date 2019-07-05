using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace App.Entities.Models
{
    public class RegSiswa
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "RegSiswa Id")]
        public int RegSiswaId { get; set; }

        [Required]
        [Display(Name = "Nama Siswa")]
        public string NamaSiswa { get; set; }

        [Required]
        [Display(Name = "Nama Ayah Siswa")]
        public string NamaAyah { get; set; }

        [Required]
        [Display(Name = "Nama Ibu Siswa")]
        public string NamaIbu { get; set; }

        [Required]
        [Display(Name = "Kontak Ayah Siswa")]
        public string KontakAyah { get; set; }

        [Required]
        [Display(Name = "Kontak Ibu Siswa")]
        public string KontakIbu { get; set; }

        [Required]
        [Display(Name = "Asal Sekolah")]
        public string AsalSekolah { get; set; }

        [Required]
        [Display(Name = "Kategori Spp")]
        public string KatSpp { get; set; }

        [Display(Name = "Tipe Discount")]
        public string TypeDisc { get; set; }

        [Display(Name = "Nominal Discount")]
        public string NomDisc { get; set; }

        [Required]
        [Display(Name = "Tingkat")]
        public int? TingkatId { get; set; }

        public string Tingkat { get; set; }

        [Required]
        [Display(Name = "Periode Daftar")]
        public string PerDaftar { get; set; }

        [Display(Name = "Tahun")]
        public string Year { get; set; }

        [Required]
        [Display(Name = "Tahap Pembayaran 1")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Tahapsatu { get; set; }

        [Required]
        [Display(Name = "Tahap Pembayaran 2")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Tahapdua { get; set; }

        [Required]
        [Display(Name = "Kategori Biaya Masuk")]
        public string KatAdm { get; set; }

        [Display(Name = "Tipe Discount BM")]
        public string TypeDiscAdm { get; set; }

        [Display(Name = "Nominal Discount BM")]
        public string NomDiscAdm { get; set; }

        [Required]
        [Display(Name = "Tanggal Daftar")]
        public DateTime TglDaftar { get; set; }
    }
}