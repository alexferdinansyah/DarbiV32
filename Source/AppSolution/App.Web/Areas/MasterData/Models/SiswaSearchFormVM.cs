using App.Entities.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Web.Areas.MasterData.Models
{
    public class SiswaSearchFormVM : Controller
    {
        // GET: MasterData/SiswaSearchFormVM
        //public ActionResult Index()
        //{
        //    return View();
        //}
        [Display(Name = "No Sisda")]
        public string Nosisda { get; set; }


        [Display(Name = "Is Active")]
        public bool IsActive { get; set; } = true;
    }
}