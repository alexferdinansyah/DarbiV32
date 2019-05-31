using App.Entities.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace App.Entities.Models
{
	public class Kelas
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Display(Name = "Kelas Id")]
		public int KelasId { get; set; }

		[Required]
		[DataType(DataType.Text)]
		[Display(Name = "Kelas Name")]
		public string KelasName { get; set; }

		[Required]
		[Display(Name = "Tingkat")]
		public int Tingkat { get; set; }

		[Required]
		[Display(Name = "Jenjang")]
		public int Jenjang { get; set; }
	}
}