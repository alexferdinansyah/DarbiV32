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
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Display(Name = "History Id")]
		public int HistoryId { get; set; }

		[Display(Name = "Periode")]
		public string periode { get; set; }

		[Display(Name = "Kategori")]
		public string kategori { get; set; }

		[Display(Name = "Jenis")]
		public string jenis { get; set; }

		[Required]
		[Display(Name = "Tingkat")]
		public int tingkat { get; set; }

		[Display(Name = "Nominal")]
		public double nominal { get; set; }
	}
}