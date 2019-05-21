using App.Entities.DataAccessLayer;
using App.Web.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Web.Areas.AccessManagement.Models
{
    public class UpdateUserPasswordFormVM : IValidatableObject
    {
        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Re Type New Password")]
        public string ReTypeNewPassword { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            //var UserModules = Function.UserModules();
            //UserModules = UserModules.Where(x =>
            //    x.Area.ToLower() == "AccessManagement".ToLower()
            //    && x.Controller.ToLower() == "RequestForgotPassword".ToLower()
            //    && x.Action.ToLower() == "Index".ToLower()).ToList();

            //if (UserModules.Count() <= 0)
            //{
            //    if (Username != HttpContext.Current.User.Identity.Name)
            //    {
            //        yield return new ValidationResult("You dont have access this email!", new[] { "Username" });
            //    }
            //}

            if (NewPassword != ReTypeNewPassword)
            {
                yield return new ValidationResult("Not match!", new[] { "ReTypeNewPassword" });
            }
        }
    }
}