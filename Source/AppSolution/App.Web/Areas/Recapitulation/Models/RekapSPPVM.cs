using App.Entities.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Web.Areas.Recapitulation.Models
{
    public class RekapSPPVM
    {
        public string Nosisda { get; set; }

        [Display(Name = "Nama Siswa")]
        public string Namasiswa { get; set; }

        [Display(Name = "Kelas Tingkat")]
        public string Kelastingkat { get; set; }

        [Display(Name = "Tingkat")]
        public string tingkat { get; set; }

        [Display(Name = "Jenjang")]
        public string Jenjang { get; set; }

        [Display(Name = "Bulan SPP")]
        public string bulanspp { get; set; }

        [Display(Name = "Bayar SPP")]
        public string bayarspp { get; set; }

        [Display(Name = "Tanggal Bayar")]
        //Perubahan By Billy
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy }")]
        //Aslinya [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        [DataType(DataType.DateTime)]
        public DateTime? tglbayar { get; set; }

        [Display(Name = "Tanggal Transfer")]
        //Perubahan By Billy
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy }")]
        //Aslinya [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        [DataType(DataType.DateTime)]
        public DateTime? tgltransfer { get; set; }

        [Display(Name = "Tipe Bayar")]
        public string tipebayar { get; set; }

        [Display(Name = "Nama Bank")]
        public string namabank { get; set; }

        [Display(Name = "Nominal")]
        public string nominal { get; set; }

        [Display(Name = "Kasir")]
        public string Username { get; set; }
        public int? UserId { get; set; }
    }
}