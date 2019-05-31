using App.Entities.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Web.Areas.MasterData.Models
{
    public class EditTingkatFormVM
    {
		public int TingkatId { get; set; }

		[Required]
        [DataType(DataType.Text)]
        [Display(Name = "Nama tingkat")]
        public string Namatingkat { get; set; }

        [Display(Name = "Jenjang")]
        public int? JenjangId { get; set; }

        public SelectList Jenjangs()
        {
            DatabaseContext db = new DatabaseContext();
            var Jenjangs = db.Jenjangs;

            return new SelectList(Jenjangs.ToList(), "JenjangId", "JenjangName", "0");
        }
    }
}