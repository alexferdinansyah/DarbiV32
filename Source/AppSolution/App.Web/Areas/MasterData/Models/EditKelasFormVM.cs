using App.Entities.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Web.Areas.MasterData.Models
{
    public class EditKelasFormVM
	{
		public int KelasId { get; set; }

		[Required]
		[DataType(DataType.Text)]
		[Display(Name = "Kelas Name")]
		public string KelasName { get; set; }

		[Display(Name = "Tingkat")]
		public int? TingkatId { get; set; }

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