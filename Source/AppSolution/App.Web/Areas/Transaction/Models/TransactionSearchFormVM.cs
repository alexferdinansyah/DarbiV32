using App.Entities.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Web.Areas.Transaction.Models
{
    public class TransactionSearchFormVM : Controller
    {
        // GET: Transaction/TransactionSearchFormVM
        [Display(Name = "Nama Siswa")]
        public string NamaSiswa { get; set; }
        public Boolean? isAdmin { get; set; }
    }
}