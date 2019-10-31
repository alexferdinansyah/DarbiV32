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
		// GET: MasterData/Kelas
		public ActionResult Index(SearchKelas model = null)
		{
			if (model == null)
			{
				model = new SearchKelas();
				//model.IsActive = true;
			}

			return View(model);
		}
		//GET : MasterData/Kelas/Create
		public ActionResult Create()
		{
			CreateKelas model = new CreateKelas();

			return View(model);
		}

		//POST : MasterData/Kelas/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(CreateKelas model)
		{
			if (ModelState.IsValid)
			{
				Kelas newmodel = new Kelas();
				newmodel.KelasName = model.KelasName;
                newmodel.TingkatId = model.TingkatId;
				db.Kelass.Add(newmodel);
				db.SaveChanges();
				return RedirectToAction("Index");
			}

			return View(model);
		}
		[HttpGet]
		public ActionResult AjaxKelas(JQueryDataTableParamModel param)
		{
			var QS = Request.QueryString;
			String KelasName = QS["KelasName"];
            String Namatingkat = QS["Namatingkat"];
			
			//Boolean IsActive = (QS["IsActive"] == "false" ? false : true);

			List<string[]> listResult = new List<string[]>();
			String errorMessage = "";

			try
			{
				IEnumerable<Kelas> Query = db.Kelass;
				  if ((KelasName != "" ) || (Namatingkat != ""))
				{
					Query = Query.Where(x => x.KelasName.Contains(KelasName) || x.Namatingkat.Equals(Namatingkat));
				}
                

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
						data.Namatingkat.Namatingkat + " - " + data.Namatingkat.Jenjangs.JenjangName,
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

		// GET: MasterData/Bank/Delete/5
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Kelas kelas = db.Kelass.Find(id);
			if (kelas == null)
			{
				return HttpNotFound();
			}

			return View(kelas);
		}

		// POST: MasterDataBank/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			Kelas kelas = db.Kelass.Find(id);
			db.Kelass.Remove(kelas);
			db.SaveChanges();
			return RedirectToAction("Index");
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}

		// GET: AccessManagement/User/Edit/5
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Kelas kelas = db.Kelass.Find(id);
			if (kelas == null)
			{
				return HttpNotFound();
			}

			EditKelasFormVM editmodel = new EditKelasFormVM();
			editmodel.KelasId = kelas.KelasId;
			editmodel.KelasName = kelas.KelasName;
            editmodel.TingkatId = kelas.TingkatId;
			//editmodel. = tingkat.Jenjangs;

			return View(editmodel);
		}

		// POST: AccessManagement/User/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "KelasId,KelasName,TingkatId")] Kelas kelas)
		{
			if (ModelState.IsValid)
			{
				Kelas KelasCek = db.Kelass.Find(kelas.KelasId);
				KelasCek.KelasName = kelas.KelasName;
                 KelasCek.TingkatId = kelas.TingkatId;

				db.Entry(KelasCek).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(kelas);
		}

		// GET: MasterData/Bank/Details/5
		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Kelas kelas = db.Kelass.Find(id);
			if (kelas == null)
			{
				return HttpNotFound();
			}
			return View(kelas);
		}
	}
}