using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using App.Entities.DataAccessLayer;
using App.Entities.Models;
using App.Web.Models;
using App.Web.Areas.MasterData.Models;
using App.Entities;

namespace App.Web.Areas.MasterData.Controllers
{
    [ControllerAuthorize]
    public class BankController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: MasterData/Bank
        public ActionResult Index(BankSearchFormVM model = null)
        {
            if (model == null)
            {
                model = new BankSearchFormVM();
                //model.IsActive = true;
            }

            return View(model);
        }

        //GET : MasterData/Bank/Create
        public ActionResult Create()
        {
            return View();
        }

        //POST : MasterData/Bank/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Bank bank)
        {
            if (ModelState.IsValid)
            {
                db.Banks.Add(bank);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bank);
        }


    }
}