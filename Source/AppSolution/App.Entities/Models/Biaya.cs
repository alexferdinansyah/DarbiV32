﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Entities.Models
{
    public class Biaya
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Tingkat Id")]
        public int BiayaId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Kategori")]
        public string KatBiaya { get; set; }

        [Display(Name = "Jenis")]
        public string JenisBiaya { get; set; }

        [Display(Name = "Nominal")]
        public string NomBiaya { get; set; }

        [Display(Name = "Tingkat")]
        public int? TingkatId { get; set; }

        public string Tingkat { get; set; }

        [Display(Name = "Jenjang")]
        public int? JenjangId { get; set; }

        public virtual Jenjang Jenjangs { get; set; }

        [Display(Name = "School Support")]
        public int? SsId { get; set; }
        public string JenisSS { get; set; }
    }
}