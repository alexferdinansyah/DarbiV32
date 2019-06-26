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
        [Display(Name = "Nama Lengkap Siswa")]
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

        [Required]
        [Display(Name = "Tingkat")]
        public int? TingkatId { get; set; }

        public string Tingkat { get; set; }

        [Required]
        [Display(Name = "Periode Daftar")]
        public string PerDaftar { get; set; }

        [Required]
        [Display(Name = "Gelombang Test")]
        public string GelTest { get; set; }

        [Required]
        [Display(Name = "Tahap 1")]
        public DateTime Tahapsatu { get; set; }

        [Required]
        [Display(Name = "Tahap 2")]
        public DateTime Tahapdua { get; set; }

        [Required]
        [Display(Name = "Kategori Administrasi")]
        public string KatAdm { get; set; }

        [Required]
        [Display(Name = "Tanggal Daftar")]
        public DateTime TglDaftar { get; set; }
    }
}