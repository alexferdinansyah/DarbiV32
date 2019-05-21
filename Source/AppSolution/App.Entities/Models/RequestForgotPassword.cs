using App.Entities.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace App.Entities.Models
{
    public class RequestForgotPassword : IValidatableObject
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Request Forgot Password Id")]
        public int RequestForgotPasswordId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Username")]
        public string Username { get; set; }


        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Request Date")]
        public DateTime RequestDate { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            DatabaseContext db = new DatabaseContext();

            User User = db.Users.Where(x=> x.Username == Username).FirstOrDefault();

            if (User == null)
            {
                yield return new ValidationResult("Username Not Found!", new[] { "Username" });
            }
        }
    }    
}