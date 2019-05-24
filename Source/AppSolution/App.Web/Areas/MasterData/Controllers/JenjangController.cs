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
	public class JenjangController : Controller
	{
		private DatabaseContext db = new DatabaseContext();

		// GET: MasterData/jenjang
		public ActionResult Index(SearchJenjang model = null)
		{
			if (model == null)
			{
				model = new SearchJenjang();
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
		public ActionResult Create(Jenjang jenjang)
		{
			if (ModelState.IsValid)
			{
				db.Jenjangs.Add(jenjang);
				db.SaveChanges();
				return RedirectToAction("Index");
			}

			return View(jenjang);
		}
		[HttpGet]
		public ActionResult AjaxJenjang(JQueryDataTableParamModel param)
		{
			var QS = Request.QueryString;
			String JenjangName = QS["JenjangName"];
			//Boolean IsActive = (QS["IsActive"] == "false" ? false : true);

			List<string[]> listResult = new List<string[]>();
			String errorMessage = "";

			try
			{
				IEnumerable<Jenjang> Query = db.Jenjangs;
				if (JenjangName != "")
				{
					Query = Query.Where(x => x.JenjangName.Contains(JenjangName));
				}

				//Query = Query.Where(x => x.IsActive == IsActive);

				int TotalRecord = Query.Count();

				var OrderedQuery = Query.OrderBy(x => x.JenjangId);

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
						data.JenjangName,
                        //(data.IsActive == true ? "<input type=\"checkbox\" disabled checked>" : "<input type=\"checkbox\" disabled>"),
                        data.JenjangId.ToString()
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