using App.Entities.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace App.Entities.Models
{
    public class RekapBiayaMasuk 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "RekapBiaya Id")]
        public int RekapBiayaId { get; set; }
        public string Nosisda { get; set; }
        [Display(Name = "Nama Siswa")]
        public string Namasiswa { get; set; }
        public string Jenjang { get; set; }
        public string Tingkat { get; set; }
        public string periode { get; set; }
        public string tanggalhistory { get; set; }
    }
}