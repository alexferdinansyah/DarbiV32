using App.Entities.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace App.Entities.Models
{
	public class Jenjang
	{
			[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
			[Display(Name = "Jenjang Id")]
			public int JenjangId { get; set; }

			[Required]
			[DataType(DataType.Text)]
			[Display(Name = "Jenjang Name")]
			public string JenjangName { get; set; }
    }
}