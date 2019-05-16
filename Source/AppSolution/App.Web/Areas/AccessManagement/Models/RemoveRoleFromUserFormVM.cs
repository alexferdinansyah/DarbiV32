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
    public class RemoveRoleFromUserFormVM
    {

        public int RoleId { get; set; }

        public int UserId { get; set; }

        public Role Role { get; set; }

        public User User { get; set; }
    }
}