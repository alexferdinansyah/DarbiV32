using App.Entities.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace App.Entities.Models
{
    public class Tingkat
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Tingkat Id")]
        public int TingkatId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Nama tingkat")]
        public string Namatingkat { get; set; }

		[Display(Name = "Jenjang Id")]
		public int? JenjangId { get; set; }

		[Display(Name = "Jenjang")]
		public virtual Jenjang Jenjangs { get; set; }

	}

}