using App.Entities;
using App.Entities.DataAccessLayer;
using App.Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace App.Web.Models
{

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel : IValidatableObject
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        public User User { get; set; } = null;
        
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!String.IsNullOrEmpty(Email) && !String.IsNullOrEmpty(Password))
            {
                DatabaseContext db = new DatabaseContext();
                String PasswordHash = Security.GetHashString(Password);
                User = db.Users.Where(x => x.Email == Email && x.Password == PasswordHash).FirstOrDefault();
                if (User == null)
                {
                    yield return new ValidationResult("Invalid email or Password", new[] { "Password" });
                }
            }

        }
    }
    

    public class ForgotPasswordViewModel : IValidatableObject
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            DatabaseContext db = new DatabaseContext();

            User User = db.Users.Where(x => x.Email == Email).FirstOrDefault();

            if (User == null)
            {
                yield return new ValidationResult("Email Not Found!", new[] { "Email" });
            }
        }
    }
}
