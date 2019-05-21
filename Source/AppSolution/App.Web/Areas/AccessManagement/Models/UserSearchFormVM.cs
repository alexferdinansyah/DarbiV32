using App.Entities.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace App.Web.Areas.AccessManagement.Models
{
    public class UserSearchFormVM 
    {
        [Display(Name = "Username")]
        public string Username { get; set; }

        //[Display(Name = "Is Active")]
        //public bool IsActive { get; set; } = true;
    }
}