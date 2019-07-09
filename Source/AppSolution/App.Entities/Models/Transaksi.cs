using App.Entities.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace App.Entities.Models
{
    public class Transaksi
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Transaksi Id")]
        public int TransId { get; set; }

        [Display(Name = "No Sisda")]
        public string Nosisda { get; set; }

        [Display(Name = "Total Pembayaran")]
        public string totalBM { get; set; }

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