using App.Entities.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Web.Areas.Recapitulation.Models
{
    public class RekapSchoolSupportVM : Controller
    {
        public string Nosisda { get; set; }

        [Display(Name = "Nama Siswa")]
        public string Namasiswa { get; set; }

        [Display(Name = "Kelas Tingkat")]
        public string Kelastingkat { get; set; }

        [Display(Name = "Jenjang")]
        public string Jenjang { get; set; }

        [Display(Name = "Biaya Masuk")]
        public string biayaBM { get; set; }

        [Display(Name = "Tanggal Bayar")]
        public DateTime? tglbayar { get; set; }

        [Display(Name = "School Support")]
        public string SSId { get; set; }

        [Display(Name = "School Support")]
        public string SSName { get; set; }

        [Display(Name = "Nominal")]
        public string nominal { get; set; }
    }
}