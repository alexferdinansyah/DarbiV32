using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace App.Entities.Models
{
    public class Module
    {

        public Module()
        {
            this.Roles = new HashSet<Role>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Module Id")]
        public int ModuleId { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Area")]
        public string Area { get; set; }

        [Required]
        [Display(Name = "Controller")]
        public string Controller { get; set; }

        [Required]
        [Display(Name = "Action")]
        public string Action { get; set; }

        [Required]
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }


        public virtual ICollection<Role> Roles { get; set; }
    }
}