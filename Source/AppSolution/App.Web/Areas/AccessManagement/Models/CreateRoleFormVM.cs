using App.Entities.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Web.Areas.AccessManagement.Models
{
    public class CreateRoleFormVM
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
        
        [Display(Name = "Parent Role")]
        public int? RoleIdParent { get; set; }

        public SelectList Roles()
        {
            DatabaseContext db = new DatabaseContext();
            var Roles = db.Roles;

            return new SelectList(Roles.ToList(), "RoleId", "Name", "0");
        }
    }
}