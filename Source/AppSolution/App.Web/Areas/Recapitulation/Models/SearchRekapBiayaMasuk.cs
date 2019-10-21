using App.Entities.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Web.Areas.Recapitulation.Models
{ 
    public class SearchRekapBiayaMasuk 
    {
        [Display(Name = "Nama Siswa")]
        public string Namasiswa { get; set; }

        public string Nosisda { get; set; }

        [Display(Name = "Tanggal Pembayaran")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy hh-mm}")]
        [DataType(DataType.DateTime)]
        //[DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? tglbayar { get; set; }

        public string Opsi { get; set; }

        [Display(Name = "Jenjang")]
        public int? JenjangId { get; set; }
        public int? Jenjang {get; set;}

        [Display(Name = "School Support")]
        public string JenisSS { get; set; }

        public int? UserId { get; set; }
        [Display(Name = "Kasir")]
        public string Username { get; set; }

        public SelectList Jenjangs()
        {
            DatabaseContext db = new DatabaseContext();
            var Jenjangs = db.Jenjangs;

            return new SelectList(Jenjangs.ToList(), "JenjangId", "JenjangName", "0");
        }
        public SelectList Users()
        {
            DatabaseContext db = new DatabaseContext();
            var Users = db.Users;

            return new SelectList(Users.ToList(), "UserId", "Username", "0");
        }
    }
}