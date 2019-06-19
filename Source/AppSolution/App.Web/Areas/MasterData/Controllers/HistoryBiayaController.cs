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
    public class HistoryBiayaController : Controller
    {
		// GET: MasterData/HistoryBiaya
		public ActionResult Index(SearchHistoryBiayaFormVM model = null)
		{
			if (model == null)
			{
				model = new SearchHistoryBiayaFormVM();
				//model.IsActive = true;
			}

			return View(model);
		}

	}
}