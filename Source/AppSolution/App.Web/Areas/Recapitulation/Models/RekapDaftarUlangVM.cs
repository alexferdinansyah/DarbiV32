using App.Entities.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Web.Areas.Recapitulation.Models
{
    public class RekapDaftarUlangVM
    {
        public string Nosisda { get; set; }

        [Display(Name = "Nama Siswa")]
        public string Namasiswa { get; set; }

        [Display(Name = "Kelas Tingkat")]
        public string Kelastingkat { get; set; }

        [Display(Name = "Tingkat")]
        public string tingkat { get; set; }

        [Display(Name = "Jenjang")]
        public string jenjang { get; set; }

        [Display(Name = "Total Daftar Ulang")]
        public string daftarUlang { get; set; }
        [Display(Name = "Daftar Ulang Yang Telah Dibayar")]
        public string cicilDaftarUlang { get; set; }
        [Display(Name = "Bayar Daftar Ulang")]
        public string bayarDaftarUlang { get; set; }

        [Display(Name = "Tanggal Bayar")]
        public DateTime? tglbayar { get; set; }

        [Display(Name = "Tanggal Transfer")]
        public DateTime? tgltransfer { get; set; }

        [Display(Name = "Tipe Bayar")]
        public string tipebayar { get; set; }

        [Display(Name = "Nama Bank")]
        public string namabank { get; set; }
    }
}