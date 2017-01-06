using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace gbsExtranetMVC.Models
{
    public class UsersExt
    {
        public long UserID { get; set; }
        [Required(ErrorMessage = "Please Enter User FullName")]
        public string Fullname { get; set; }
        [Required(ErrorMessage = "Please Enter Username")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Please Enter Password")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Please Select User Role")]
        public long RoleID { get; set; }
        public string UserRole { get; set; }
        [Required(ErrorMessage = "Please Enter Email Address")]
        [EmailAddress(ErrorMessage = "Please Enter Valid Email Address")]
        public string EmailAddress { get; set; }

        public byte[] RowVersion_Byte { get; set; }
        public string RowVersion_Str { get; set; }

        public bool Locked { get; set; }

        public UsersExt()
        {
            Locked = false;
        }
    }
}