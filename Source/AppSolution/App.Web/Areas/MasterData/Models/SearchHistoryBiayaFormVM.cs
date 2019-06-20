using App.Entities.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Web.Areas.MasterData.Models
{
    public class SearchHistoryBiayaFormVM : Controller
    {
		// GET: MasterData/SearchBiayaHistoryFormVM

		[Display(Name = "Periode")]
		public string PeriodeHB { get; set; }
	}
}