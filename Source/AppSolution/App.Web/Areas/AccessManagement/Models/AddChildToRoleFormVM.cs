using App.Entities.DataAccessLayer;
using App.Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Web.Areas.AccessManagement.Models
{
    public class AddChildToRoleFormVM : IValidatableObject
    {

        [Display(Name = "Role Child")]
        public int RoleIdChild { get; set; }

        public int RoleId { get; set; }
        
        public SelectList Roles()
        {
            DatabaseContext db = new DatabaseContext();
            var Roles = db.Roles;

            return new SelectList(Roles.ToList(), "RoleId", "Name");
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            DatabaseContext db = new DatabaseContext();

            var Role = db.Roles.Find(RoleId);

            var Roles = Role.Roles.Where(x => x.RoleId == RoleIdChild);
            if (Roles.Count() > 0)
            {
                yield return new ValidationResult("Role already added!", new[] { "RoleIdChild" });
            }
            var Parent = db.Roles.Where(x => x.Roles.Any(c => c.RoleId == RoleIdChild));
            if (Parent.Count() > 0)
            {
                yield return new ValidationResult("Role was have parent!", new[] { "RoleIdChild" });
            }

            if (RoleId == RoleIdChild)
            {
                yield return new ValidationResult("Cannot add self role!", new[] { "RoleIdChild" });
            }            
        }
    }
}