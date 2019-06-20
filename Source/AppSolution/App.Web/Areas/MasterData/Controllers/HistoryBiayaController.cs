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
        private DatabaseContext db = new DatabaseContext();
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

        [HttpGet]
        public ActionResult AjaxHistory(JQueryDataTableParamModel param)
        {
            var QS = Request.QueryString;
            String PeriodeHB = QS["PeriodeHB"];
            //Boolean IsActive = (QS["IsActive"] == "false" ? false : true);

            List<string[]> listResult = new List<string[]>();
            String errorMessage = "";

            try
            {
                IEnumerable<HistoryBiaya> Query = db.HistoryBiayas;
                if (PeriodeHB != "")
                {
                    Query = Query.Where(x => x.PeriodeHB.Contains(PeriodeHB));
                }

                //Query = Query.Where(x => x.IsActive == IsActive);

                int TotalRecord = Query.Count();

                var OrderedQuery = Query.OrderBy(x => x.HistoryId);

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
                        data.PeriodeHB,
                        data.KategoriHB,
                        data.JenisHB,
                        data.TingkatHB.ToString(),
                        data.NomHisBiaya.ToString(),
                        //(data.IsActive == true ? "<input type=\"checkbox\" disabled checked>" : "<input type=\"checkbox\" disabled>"),
                        data.HistoryId.ToString()
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