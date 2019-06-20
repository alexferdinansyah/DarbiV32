using App.Entities.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace App.Entities.Models
{
	public class HistoryBiaya
	{
        [Key]
		[Display(Name = "History Id")]
		public int HistoryId { get; set; }

        [Required]
        [Display(Name = "Periode")]
		public string PeriodeHB { get; set; }

        [Required]
        [Display(Name = "Kategori")]
		public string KategoriHB { get; set; }

        [Required]
        [Display(Name = "Jenis")]
		public string JenisHB { get; set; }

		[Required]
		[Display(Name = "Tingkat")]
		public int TingkatHB { get; set; }

        [Required]
        [Display(Name = "Nominal")]
		public double NomHisBiaya { get; set; }
	}
}