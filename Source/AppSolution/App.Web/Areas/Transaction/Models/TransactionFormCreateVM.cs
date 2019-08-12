using App.Entities.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Web.Areas.Transaction.Models
{
    public class TransactionFormCreateVM : Controller
    {
        [Display(Name = "Transaksi Id")]
        public int TransId { get; set; }

        [Display(Name = "No Sisda")]
        public string Nosisda { get; set; }
        public string Namasiswa { get; set; }

        [Display(Name = "Kelas")]
        public string Kelastingkat { get; set; }

        //[DisplayFormat(DataFormatString = "{0:N2}")]
        [Display(Name = "Total Pembayaran Biaya Masuk")]
        public string totalBM { get; set; }

        [Display(Name = "Biaya Masuk Yang sudah Dibayarkan")]
        public string paidBM { get; set; }

        [Display(Name = "Bayar Biaya Masuk")]
        public string bayarBM { get; set; }

        [Display(Name = "Sisa Tagihan")]
        public string sisaTagihanBM { get; set; }

        [Display(Name = "Periode")]
        public string periode { get; set; }

        [Display(Name = "Bulan SPP")]
        public string bulanspp { get; set; }

        [Display(Name = "Bayar SPP")]
        public string bayarspp { get; set; }

        [Display(Name = "Tipe Bayar")]
        public string tipebayar { get; set; }

        [Display(Name = "Komite Sekolah")]
        public string komiteSekolah { get; set; }

        [Display(Name = "Total Daftar Ulang")]
        public string daftarUlang { get; set; }
        [Display(Name = "Daftar Ulang Yang Telah Dibayar")]
        public string cicilDaftarUlang { get; set; }
        [Display(Name = "Bayar Daftar Ulang")]
        public string bayarDaftarUlang { get; set; }
        [Display(Name = "Sisa Tagihan")]
        public string sisaTagihanDU { get; set; }

        [Display(Name = "Tanggal Transfer")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? tgltransfer { get; set; }

        [Display(Name = "Tanggal Pembayaran")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? tglbayar { get; set; }

        [Display(Name = "Bank")]
        public int? BankId { get; set; }

        public string Banknm { get; set; }

        public SelectList Bank()
        {
            DatabaseContext db = new DatabaseContext();
            var banks = db.Banks;
            return new SelectList(banks.ToList(), "BankId", "Bankname", "0");
        }

        [Display(Name = "School Support")]
        public int? SSId { get; set; }
        public int? JenisSS
        {
            get; set;
        }
        public SelectList SchoolSupport()
        {
            DatabaseContext db = new DatabaseContext();
            var SchoolSupports = db.SchoolSupports;

            return new SelectList(SchoolSupports.ToList(), "SSId", "JenisSS", "0");
        }
        //school support//
        [Display(Name = "Nominal")]
        public string nominal { get; set; }
        public SelectList Biaya()
        {
            DatabaseContext db = new DatabaseContext();
            var Biayas = db.Biayas;
            return new SelectList(Biayas.ToList(), "JenisBiaya", "NomBiaya", "0");
        }
        [Display(Name = "Uang Bayar")]
        public string uang { get; set; }

        [Display(Name = "Total Keseluruhan")]
        public string total { get; set; }
    }
}