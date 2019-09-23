using App.Entities.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Web.Areas.Recapitulation.Models
{
    public class RekapPrintVM
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

        [Display(Name = "Biaya Masuk")]
        public string biayaBM { get; set; }

        [Display(Name = "Total Biaya Masuk")]
        public string totalbm { get; set; }

        [Display(Name = "Bulan SPP")]
        public string bulanspp { get; set; }

        [Display(Name = "Bayar SPP")]
        public string bayarspp { get; set; }

        [Display(Name = "School Support")]
        public string SSId { get; set; }

        [Display(Name = "School Support")]
        public string SSName { get; set; }

        [Display(Name = "Nominal")]
        public string nominal { get; set; }

        [Display(Name = "Tanggal Bayar")]
        public DateTime? tglbayar { get; set; }

        [Display(Name = "Tanggal Transfer")]
        public DateTime? tgltransfer { get; set; }

        [Display(Name = "Tipe Bayar")]
        public string tipebayar { get; set; }

        [Display(Name = "Nama Bank")]
        public string namabank { get; set; }

        [Display(Name = "Kasir")]
        public string Username { get; set; }
        public int? UserId { get; set; }
    }
}