using App.Entities.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Web.Areas.MasterData.Models
{
	public class CreateKelas
	{

		[Display(Name = "Nama Kelas")]
		public string KelasName { get; set; }

		[Display(Name = "Tingkat")]
		public int? TingkatId { get; set; }

		public int JenjangId { get; set; }

		[Display(Name = "Jenjang")]
		public string JenjangName { get; set; }

		public SelectList Namatingkat()
		{
			DatabaseContext db = new DatabaseContext();
			var Namatingkat = db.Tingkats;

			return new SelectList(Namatingkat.ToList(), "TingkatId", "Namatingkat", "0");
		}
	}
}