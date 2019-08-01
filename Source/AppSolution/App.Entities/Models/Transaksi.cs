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
        [Display(Name = "Nama")]
        public string Namasiswa { get; set; }
        [Display(Name = "Kelas")]
        public string Kelastingkat { get; set; }

        [Display(Name = "Total Biaya Masuk")]
        public string totalBM { get; set; }

        [Display(Name = "Bayar Biaya Masuk")]
        public int? bayarBM { get; set; }

        [Display(Name = "Periode")]
        public string periode { get; set; }

        [Display(Name = "Bulan SPP")]
        public string bulanspp { get; set; }

        [Display(Name = "Bayar SPP")]
        public int? bayarspp { get; set; }

        [Display(Name = "Tipe Bayar")]
        public string tipebayar { get; set; }

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
        [Display(Name = "Nama Bank")]
        public string Banknm { get; set; }

        [Display(Name = "School Support")]
        public int? SSId { get; set; } //id schoolsupport
        public string JenisSS { get; set; } //jenis schoolsupport

        [Display(Name = "Nominal")]
        public string nominal { get; set; }

        public string infospp { get; set; }

         //Daftar Ulang
        [Display(Name = "Total Daftar Ulang")]
        public string daftarUlang { get; set; }
        [Display(Name = "Daftar Ulang Yang Telah Dibayar")]
        public int? cicilDaftarUlang { get; set; }

        [Display(Name = "Canceled")]
        public bool isCanceled { get; set; }
        [Display(Name = "Canceled By")]
        public string canceledBy { get; set; }
        [Display(Name = "Canceled Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime? canceledDate { get; set; }
    }

}