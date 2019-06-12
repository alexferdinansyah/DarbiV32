using App.Entities.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace App.Entities.Models
{
    public class SchoolSupport
    {
        [Key]
        [Display(Name = "SS Id")]
        public int SsId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Jenis School Support")]
        public string JenisSS { get; set; }
    }
}