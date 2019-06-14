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

        [DataType(DataType.Text)]
        [Display(Name = "Kategori")]
        public string KatBiaya { get; set; }

        [Display(Name = "Tingkat Id")]
        public int? TingkatId { get; set; }

        [Display(Name = "Tingkat")]
        public virtual Tingkat Tingkats { get; set; }
    }
}