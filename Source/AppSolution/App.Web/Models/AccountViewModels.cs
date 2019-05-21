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
        [Display(Name = "Username")]
        public string Username { get; set; }
    }

    public class LoginViewModel : IValidatableObject
    {
        [Required]
        [Display(Name = "Username")]
        [DataType(DataType.Text)]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        public User User { get; set; } = null;
        
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!String.IsNullOrEmpty(Username) && !String.IsNullOrEmpty(Password))
            {
                DatabaseContext db = new DatabaseContext();
                String PasswordHash = Security.GetHashString(Password);
                User = db.Users.Where(x => x.Username == Username && x.Password == PasswordHash).FirstOrDefault();
                if (User == null)
                {
                    yield return new ValidationResult("Invalid username or Password", new[] { "Password" });
                }
            }

        }
    }
    

    public class ForgotPasswordViewModel : IValidatableObject
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Username")]
        public string Username { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            DatabaseContext db = new DatabaseContext();

            User User = db.Users.Where(x => x.Username == Username).FirstOrDefault();

            if (User == null)
            {
                yield return new ValidationResult("Username Not Found!", new[] { "Username" });
            }
        }
    }
}
