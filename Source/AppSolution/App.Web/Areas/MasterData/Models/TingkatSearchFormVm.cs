using App.Entities.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace App.Web.Areas.MasterData.Models
{
    public class TingkatSearchFormVM : Controller
    {

        [Display(Name = "Nama Tingkat")]
        public string Namatingkat { get; set; }

        //[Display(Name = "Jenjang")]
        //public string Jenjang { get; set; }
    }
}