using App.Entities.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Web.Areas.Recapitulation.Models
{
    public class RekapBiayaMasukVM : Controller
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

        [Display(Name = "Biaya Masuk")]
        public string biayaBM { get; set; }

        [Display(Name = "Total Biaya Masuk")]
        public string totalbm { get; set; }

        [Display(Name = "Tanggal Bayar")]
        public string tglbayar { get; set; }

        [Display(Name = "Tipe Bayar")]
        public string tipebayar { get; set; }

        [Display(Name = "Nama Bank")]
        public string namabank { get; set; }

    }
}