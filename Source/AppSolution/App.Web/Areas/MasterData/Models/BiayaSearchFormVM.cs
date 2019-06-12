using App.Entities.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Web.Areas.MasterData.Models
{
    public class BiayaSearchFormVM : Controller
    {
        // GET: MasterData/BiayaSearchFormVM
        //public ActionResult Index()
        //{
        //    return View();
        //}
        [Display(Name = "Kategori")]
        public string KatBiaya { get; set; }
    }
}