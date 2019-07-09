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
using App.Web.Areas.Recapitulation.Models;
using App.Entities;

namespace App.Web.Areas.Recapitulation.Controllers
{

    public class RekapBiayaMasukController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: Recapitulation/RekapBiayaMasuk
        public ActionResult Index(SearchRekapBiayaMasuk model = null)
        {
            if (model == null)
            {
                model = new SearchRekapBiayaMasuk();
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult AjaxRekapBiayaMasuk(JQueryDataTableParamModel param)
        {
            var QS = Request.QueryString;
            String Namasiswa = QS["Namasiswa"];
            String Jenjang = QS["Jenjang"];
            //Boolean IsActive = (QS["IsActive"] == "false" ? false : true);

            List<string[]> listResult = new List<string[]>();
            String errorMessage = "";

            try
            {
                IEnumerable<RekapBiayaMasuk> Query = db.RekapBiayaMasuks;
                if (Namasiswa != "")
                {
                    Query = Query.Where(x => x.Namasiswa.Contains(Namasiswa));
                }
                //if (Jenjang !="")
                //{
                //    Query = Query.Where(x => x.Jenjang.Contains(Jenjang));
                //}
                int TotalRecord = Query.Count();

                var OrderedQuery = Query.OrderBy(x => x.RekapBiayaId);

                int pageSize = param.iDisplayLength;
                int pageNumber = param.iDisplayStart == 0 ? 1 : (param.iDisplayStart / param.iDisplayLength) + 1;
                var PagedQuery = OrderedQuery.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();

                int i = 0;
                foreach (var data in PagedQuery)
                {
                    i++;
                    listResult.Add(new string[]
                    {
                        i.ToString(),
                        data.Nosisda,
                        data.Namasiswa,
                        data.Jenjang,
                        data.Tingkat,
                        data.periode,
                        data.tanggalhistory,
                        //data.Jenjang,
                        //(data.IsActive == true ? "<input type=\"checkbox\" disabled checked>" : "<input type=\"checkbox\" disabled>"),
                        data.RekapBiayaId.ToString()
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