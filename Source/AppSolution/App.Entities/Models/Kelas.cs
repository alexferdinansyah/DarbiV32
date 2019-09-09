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
		[Display(Name = "Nama Kelas")]
		public string KelasName { get; set; }

		//[Required]
		[Display(Name = "Tingkat Id")]
		public int? TingkatId { get; set; }

        public string Tingkat { get; set; }

		[Display(Name = "Nama Tingkat")]
		public virtual Tingkat Namatingkat { get; set; }

        //public string Jenjang { get; set; }

		[Display(Name = "Jenjang Id")]
		public int JenjangId { get; set; }

		[Display(Name = "Jenjang")]
		public string  JenjangName { get; set; }
	}
}