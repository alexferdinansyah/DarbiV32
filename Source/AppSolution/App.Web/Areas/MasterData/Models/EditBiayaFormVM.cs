using App.Entities.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace App.Web.Areas.MasterData.Models
{
    public class EditBiayaFormVM
    {
        // GET: MasterData/EditBiayaFormVM
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Kategori")]
        public string KatBiaya { get; set; }

        public int BiayaId { get; set; }

        public IEnumerable<SelectListItem> Biayas { get; set; }

        [Display(Name = "Jenis")]
        public string JenisBiaya { get; set; }

        [Display(Name = "Nominal")]
        public string NomBiaya { get; set; }

        [Display(Name = "Tingkat")]
        public int? TingkatId { get; set; }

        [Display(Name = "School Support")]
        public int? SsId { get; set; }

        public SelectList Tingkats()
        {
            DatabaseContext db = new DatabaseContext();
            var Tingkats = db.Tingkats;

            return new SelectList(Tingkats.ToList(), "TingkatId", "Namatingkat", "0");
        }

        public SelectList SS()
        {
            DatabaseContext db = new DatabaseContext();
            var Sss = db.SchoolSupports;

            return new SelectList(Sss.ToList(), "SsId", "JenisSS", "0");
        }
    }
}