﻿using App.Entities.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Web.Areas.MasterData.Models
{
	public class CreateJenjang : Controller
	{
		[Required]
		[Display(Name = "Jenjang Name")]
		public string JenjangName { get; set; }
	}
}