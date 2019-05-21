using App.Entities.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Web.Areas.MasterData.Models
{
    public class BankSearchFormVM : Controller
    {
        // GET: MasterData/BankSearchForm
        //    public ActionResult Index()
        //    {
        //        return View();
        //    }

        [Display(Name = "Name")]
        public string Name { get; set; }
    }
}