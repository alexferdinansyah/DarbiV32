using App.Entities.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace App.Entities.Models
{
    public class User : IValidatableObject
    {
        public User()
        {
            this.Roles = new HashSet<Role>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "User Id")]
        public int UserId { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }


        public virtual ICollection<Role> Roles { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            DatabaseContext db = new DatabaseContext();

            if (UserId == 0)
            {
                var user = db.Users.Where(e => e.Email == Email).ToList();
                if (user.Count > 0)
                {
                    yield return new ValidationResult("Has been added! Please use another email.", new[] { "Email" });
                }                
            }
            else
            {
                var user = db.Users.Where(e => e.Email == Email && e.UserId != UserId).ToList();
                if (user.Count > 0)
                {
                    yield return new ValidationResult("Already used! Please use another email.", new[] { "Email" });
                }
            }     
        }
    }    
}