using App.Entities.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Web.Areas.MasterData.Models
{
    public class SchoollSupportSearchFormVM : Controller
    {
        // GET: MasterData/SchoollSupportSearchFormVM
        //public ActionResult Index()
        //{
        //    return View();
        //}
        [Display(Name = "Jenis School Support")]
        public string JenisSS { get; set; }
    }
}