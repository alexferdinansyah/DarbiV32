using App.Entities.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Web.Areas.MasterData.Models
{
	public class SearchKelas : Controller
	{
		[Display(Name = "Nama Kelas")]
		public string KelasName { get; set; }

		[Display(Name = "Nama Tingkat")]
		public string Namatingkat { get; set; }

		[Display(Name = "Nama Jenjang")]
		public string NamaJenjang { get; set; }
		public string TingkatName { get; set; }

		[Display(Name = "Jenjang")]
		public int? JenjangId { get; set; }
		public int? Jenjang
		{
			get; set;
		}
		public SelectList Jenjangs()
		{
			DatabaseContext db = new DatabaseContext();
			var Jenjangs = db.Jenjangs;

			return new SelectList(Jenjangs.ToList(), "JenjangId", "JenjangName", "0");
		}
	}
}