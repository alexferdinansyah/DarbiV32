using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace App.Entities.Models
{
    public class Role
    {
        public Role()
        {
            this.Users = new HashSet<User>();
            this.Modules = new HashSet<Module>();
            this.Roles = new HashSet<Role>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Role Id")]
        public int RoleId { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        public virtual ICollection<User> Users { get; set; }

        public virtual ICollection<Module> Modules { get; set; }

        public virtual ICollection<Role> Roles { get; set; }
    }
}