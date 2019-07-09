using App.Entities.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Web.Areas.Transaction.Models
{
    public class TransactionFormCreate
    {
        [Display(Name = "Transaksi Id")]
        public int TransId { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "No Sisda")]
        public string Nosisda { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Nama Siswa")]
        public string NamaSiswa { get; set; }

        [Display(Name = "Kelas")]
        public string Kelas { get; set; }

        [Display(Name = "Jenjang")]
        public string Jenjang { get; set; }
    }
}