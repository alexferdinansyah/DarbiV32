using App.Entities.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace App.Entities.Models
{
    public class Biaya
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Biaya Id")]
        public int BiayaId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Kategori")]
        public string KatBiaya { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Jenis")]
        public string JenisBiaya { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Tingkat")]
        public int TingkatBiaya { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Nominal")]
        public double NomBiaya { get; set; }
    }
}