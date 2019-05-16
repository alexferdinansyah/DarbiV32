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
    public class AddRoleToUserFormVM : IValidatableObject
    {

        [Display(Name = "Role")]
        public int RoleId { get; set; }

        public int UserId { get; set; }
        
        public SelectList Roles()
        {
            DatabaseContext db = new DatabaseContext();
            var Roles = db.Roles;

            return new SelectList(Roles.ToList(), "RoleId", "Name");
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            DatabaseContext db = new DatabaseContext();

            var User = db.Users.Find(UserId);

            var Role = User.Roles.Where(x => x.RoleId == RoleId);
            if (Role.Count() > 0)
            {
                yield return new ValidationResult("Role already added!", new[] { "RoleId" });
            }
            else
            {
                var AllRoleChileOnParentRoleUser = User.Roles.SelectMany(x => x.Roles).ToList();
                Boolean start = true;
                var AllChild = AllRoleChileOnParentRoleUser;
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
                        AllRoleChileOnParentRoleUser.AddRange(CurrentChild);
                    }                 

                } while (start);

                var RoleChild = AllRoleChileOnParentRoleUser.Where(x => x.RoleId == RoleId).FirstOrDefault();
                String ParentName = "";
                if (RoleChild != null)
                {
                    var Parent = db.Roles.Where(x => x.Roles.Any(z => z.RoleId == RoleChild.RoleId)).FirstOrDefault();
                    
                    Boolean IsRoleNotInUser = true;
                    do
                    {
                        ParentName += Parent.Name + " -> ";
                        if (User.Roles.Contains(Parent))
                        {
                            IsRoleNotInUser = false;
                        }
                        else
                        {
                            Parent = db.Roles.Where(x => x.Roles.Any(z => z.RoleId == Parent.RoleId)).FirstOrDefault();
                        }
                    } while (IsRoleNotInUser);


                    ParentName = ParentName.Remove(ParentName.Length - 4);
                    yield return new ValidationResult("Role already covered in parent role " + ParentName, new[] { "RoleId" });
                }
            }
        }
    }
}