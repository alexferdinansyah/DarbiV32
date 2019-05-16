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
    public class AddModuleToRoleFormVM : IValidatableObject
    {

        [Display(Name = "Module")]
        public int ModuleId { get; set; }

        public int RoleId { get; set; }
        
        public SelectList Module()
        {
            DatabaseContext db = new DatabaseContext();
            var Modules = db.Modules;

            return new SelectList(Modules.ToList(), "ModuleId", "Name");
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            DatabaseContext db = new DatabaseContext();

            var Role = db.Roles.Find(RoleId);

            var Modules = Role.Modules.Where(x => x.ModuleId == ModuleId);
            if (Modules.Count() > 0)
            {
                yield return new ValidationResult("Module already added!", new[] { "ModuleId" });
            }
            else
            {
                var AllRoleChildOnParentRole = Role.Roles.SelectMany(x => x.Roles).ToList();
                Boolean start = true;
                var AllChild = AllRoleChildOnParentRole;
                do
                {
                    var CurrentChild = AllChild.SelectMany(x => x.Roles).ToList();
                    if (CurrentChild.Count() <= 0)
                    {
                        start = false;
                    }
                    else
                    {
                        AllChild = CurrentChild;
                        AllRoleChildOnParentRole.AddRange(CurrentChild);
                    }

                } while (start);

                AllRoleChildOnParentRole.AddRange(Role.Roles);

                var AllModuleChildInRole = new List<Module>();
                foreach (var item in AllRoleChildOnParentRole)
                {
                    AllModuleChildInRole.AddRange(item.Modules);
                }
                if (AllModuleChildInRole.Where(x => x.ModuleId == ModuleId).Count() > 0)
                {
                    yield return new ValidationResult("Module was covered in Child role!", new[] { "ModuleId" });
                }
            }
        }
    }
}