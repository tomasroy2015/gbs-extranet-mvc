using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

//using System.Web.Mvc;

namespace gbsExtranetMVC.Models
{
    public class ChangePasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [StringLength(20, ErrorMessage = "Minimum 6 characters required", MinimumLength = 6)]
        [RegularExpression(@"^((?=.*[A-Z])(?=.*\d)).+$", ErrorMessage = "at least one Capital letter and a Number required")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password Required")]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class ForgottenPasswordModel
    {

        [Required(ErrorMessage = "Email Required")]
        [EmailAddress(ErrorMessage = "Please enter valid Email")]
        public string Email { get; set; }
    }

    public class LogOnModel
    {
        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Language")]
        public string Language { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    //    [Required]
      //  [DataType(DataType.Password)]
      //  [Display(Name = "ConfirmPassword")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public class EmailAddressAttribute : RegularExpressionAttribute
    {
        private const string pattern = @"^\w+([-+.]*[\w-]+)*@(\w+([-.]?\w+)){1,}\.\w{2,4}$";

        static EmailAddressAttribute()
        {
            // necessary to enable client side validation
            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(EmailAddressAttribute), typeof(RegularExpressionAttributeAdapter));
        }

        public EmailAddressAttribute()
            : base(pattern)
        {
        }
    }
}
