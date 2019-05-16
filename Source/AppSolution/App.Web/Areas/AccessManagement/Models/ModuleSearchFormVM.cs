using App.Entities.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace App.Web.Areas.AccessManagement.Models
{
    public class ModuleSearchFormVM
    {
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Area")]
        public string Area { get; set; }

        [Display(Name = "Controller")]
        public string ControllerName { get; set; }

        [Display(Name = "Action")]
        public string ActionName { get; set; }
        
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; } = true;
    }
}