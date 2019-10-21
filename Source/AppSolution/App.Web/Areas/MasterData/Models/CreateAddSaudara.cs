using App.Entities.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;



namespace App.Web.Areas.MasterData.Models
{
    public class CreateAddSaudara
    {
        [Required]
        [Display(Name ="Jenis Kelamin")]
        public string Sex { get; set; }
    }
}