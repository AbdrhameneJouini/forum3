using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace forum.ViewModels
{
    public class RegisterViewModel
    {
        [Display(Name = "User Name")]
        [Required(ErrorMessage = " User Name is required")]
        public string UserName { get; set; }


        [Display(Name = "Email address")]
        [Required(ErrorMessage = " Email address is required")]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Display(Name = "Confirm password")]
        [Required(ErrorMessage = "Confirm password is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password do not match")]
        public string ConfirmPassword { get; set; }


    }
}
