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
	public class KelasController : Controller
	{	
		private DatabaseContext db = new DatabaseContext();
		// GET: MasterData/jenjang
		public ActionResult Index(SearchKelas model = null)
		{
			if (model == null)
			{
				model = new SearchKelas();
				//model.IsActive = true;
			}

			return View(model);
		}
		//GET : MasterData/Jenjang/Create
		public ActionResult Create()
		{
			return View();
		}
		//POST : MasterData/Jenjang/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(Kelas kelas)
		{
			if (ModelState.IsValid)
			{
				db.Kelass.Add(kelas);
				db.SaveChanges();
				return RedirectToAction("Index");
			}

			return View(kelas);
		}
		[HttpGet]
		public ActionResult AjaxKelas(JQueryDataTableParamModel param)
		{
			var QS = Request.QueryString;
			String KelasName = QS["KelasName"];
			String Tingkat = QS["Tingkat"];
			String Jenjang = QS["Jenjang"];
			//Boolean IsActive = (QS["IsActive"] == "false" ? false : true);

			List<string[]> listResult = new List<string[]>();
			String errorMessage = "";

			try
			{
				IEnumerable<Kelas> Query = db.Kelass;
				if (KelasName != "")
				{
					Query = Query.Where(x => x.KelasName.Contains(KelasName));
				}
				if (Tingkat != "")
				{
					Query = Query.Where(x => x.KelasName.Contains(Tingkat));
				}
				if (Jenjang != "")
				{
					Query = Query.Where(x => x.KelasName.Contains(Jenjang));
				}

				//Query = Query.Where(x => x.IsActive == IsActive);

				int TotalRecord = Query.Count();

				var OrderedQuery = Query.OrderBy(x => x.KelasId);

				int pageSize = param.iDisplayLength;
				int pageNumber = param.iDisplayStart == 0 ? 1 : (param.iDisplayStart / param.iDisplayLength) + 1; ;
				var PagedQuery = OrderedQuery.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();

				int i = 0;
				foreach (var data in PagedQuery)
				{
					i++;
					listResult.Add(new string[]
					{
						i.ToString(),
						data.KelasName,
						data.KelasId.ToString()
					});
				}
				return Json(new
				{
					sEcho = param.sEcho,
					iTotalRecords = TotalRecord,
					iTotalDisplayRecords = TotalRecord,
					aaData = listResult
				},
				JsonRequestBehavior.AllowGet);
			}
			catch (Exception ex)
			{
				errorMessage = ex.Message;
			}

			return Json(new
			{
				sEcho = param.sEcho,
				iTotalRecords = 0,
				iTotalDisplayRecords = 0,
				aaData = listResult,
				error = errorMessage
			},
			JsonRequestBehavior.AllowGet);
		}
	}
}