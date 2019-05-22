using App.Entities.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace App.Entities.Models
{
    public class Bank
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Bank Id")]
        public int BankId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Bank Name")]
        public string Bankname { get; set; }
    }    
}