using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieMVC.General.Models.Account
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Please enter the FullName")]
        [Display(Name = "FullName")]
        public string FullName { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Please enter the Email Address!")]
        [EmailAddress(ErrorMessage = "Email Address u entered is wrong!")]
        public string EmailAddress { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords is not Equal!")]
        public string ConfirmPassword { get; set; }
    }
}
