using App.Entities.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Web.Areas.Recapitulation.Models
{ 

    public class SearchRekapBiayaMasuk : Controller
    {
        [Display(Name = "No Sisda")]
        public string Nosisda { get; set; }

        [Display(Name = "Nama Siswa")]
        public string Namasiswa { get; set; }

        [Display(Name = "Jenjang")]
        public string Jenjang { get; set; }

        [Display(Name = "Tingkat")]
        public string Tingkat { get; set; }

        [Display(Name = "periode")]
        public string periode { get; set; }

        [Display(Name = "tanggalhistory")]
        public string tanggalhistory { get; set; }
    }
}