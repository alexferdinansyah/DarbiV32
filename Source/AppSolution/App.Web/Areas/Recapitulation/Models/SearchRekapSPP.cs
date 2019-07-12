using App.Entities.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Web.Areas.Recapitulation.Models
{ 
    public class SearchRekapSPP : Controller
    {
        [Display(Name = "Nama Siswa")]
        public string Namasiswa { get; set; }
}