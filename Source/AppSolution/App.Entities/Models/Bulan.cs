using App.Entities.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace App.Entities.Models
{
    public class Bulan
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Bulan Id")]
        [Key]
        public int BulanId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Bulan Name")]
        public string namaBulan { get; set; }
    }
}