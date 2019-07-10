﻿using App.Entities.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Web.Areas.Transaction.Models
{
    public class TransactionFormCreateVM
    {
        [Display(Name = "Transaksi Id")]
        public int TransId { get; set; }

        [Display(Name = "No Sisda")]
        public string Nosisda { get; set; }
        public string Namasiswa { get; set; }
        public string Kelastingkat { get; set; }

        //[DisplayFormat(DataFormatString = "{0:N2}")]
        [Display(Name = "Total Pembayaran Biaya Masuk")]
        public string totalBM { get; set; }

        [Display(Name = "Biaya Masuk Yang sudah Dibayarkan")]
        public string paidBM { get; set; }

        [Display(Name = "Bayar Biaya Masuk")]
        public string bayarBM { get; set; }

        [Display(Name = "Periode")]
        public string periode { get; set; }

        [Display(Name = "Bulan SPP")]
        public string bulanspp { get; set; }

        [Display(Name = "Bayar SPP")]
        public string bayarspp { get; set; }

        [Display(Name = "Tipe Bayar")]
        public string tipebayar { get; set; }

        [Display(Name = "Tanggal Transfer")]
        public DateTime tgltransfer { get; set; }

        [Display(Name = "Tanggal Pembayaran")]
        public DateTime tglbayar { get; set; }

        [Display(Name = "Nama Bank")]
        public int namabank { get; set; }
    }
}