using System.ComponentModel.DataAnnotations;

namespace MovieMVC.General.Models.Account
{
    public class LoginViewModel
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "U have to enter Your Email!")]
        [EmailAddress(ErrorMessage = "Invalid Email Address!")]
        public string EmailAddress { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
