using App.Entities.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Web.Areas.AccessManagement.Models
{
    public class EditRoleFormVM : IValidatableObject
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        [Required]
        [Display(Name = "Role Id")]
        public int RoleId { get; set; }

        [Display(Name = "Parent Role Old")]
        public int? RoleIdParentOld { get; set; }

        [Display(Name = "Parent Role")]
        public int? RoleIdParentNew { get; set; }

        public SelectList Roles()
        {
            DatabaseContext db = new DatabaseContext();
            var Roles = db.Roles;

            return new SelectList(Roles.ToList(), "RoleId", "Name", "0");
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (RoleIdParentNew == RoleId)
            {
                yield return new ValidationResult("Cannot set self as parent!", new[] { "RoleIdParentNew" });
            }
        }
    }
}