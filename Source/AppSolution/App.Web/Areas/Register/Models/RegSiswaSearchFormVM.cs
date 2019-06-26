using App.Entities.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Web.Areas.Register.Models
{
    public class RegSiswaSearchFormVM
    {
        // GET: Register/RegSiswaSearchFormVM
        [Display(Name = "Nama Siswa")]
        public string NamaSiswa { get; set; }
    }
}