using App.Entities.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Web.Areas.AccessManagement.Models
{
    public class RoleSearchFormVM
    {
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Parent")]
        public int? RoleId { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; } = true;


        public SelectList Roles()
        {
            DatabaseContext db = new DatabaseContext();
            var Roles = db.Roles.Where(x => x.Roles.Count > 0);

            return new SelectList(Roles.ToList(), "RoleId", "Name","0");
        }
    }
}