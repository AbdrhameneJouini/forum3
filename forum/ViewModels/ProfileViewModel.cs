using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using forum.Models; // Assuming your user model is in the "forum.Models" namespace

namespace forum.ViewModels
{
    public class UniqueEmailAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var dbContext = (ForumDbContext)validationContext.GetService(typeof(ForumDbContext));

            // Get the id from the model
            var id = (string)validationContext.ObjectInstance.GetType().GetProperty("id")?.GetValue(validationContext.ObjectInstance, null);

            var existingUser = dbContext.Users.FirstOrDefault(u => u.Email == value.ToString() && u.Id != id);

            if (existingUser != null)
            {
                return new ValidationResult("Email is already in use.");
            }

            return ValidationResult.Success;
        }
    }


    public class UniqueUserNameAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var dbContext = (ForumDbContext)validationContext.GetService(typeof(ForumDbContext));

            // Get the id from the model
            var id = (string)validationContext.ObjectInstance.GetType().GetProperty("id")?.GetValue(validationContext.ObjectInstance, null);

            // Check for existing user with the same username and different id
            var existingUser = dbContext.Users.FirstOrDefault(u => u.UserName == value.ToString() && u.Id != id);

            if (existingUser != null)
            {
                return new ValidationResult("Username is already in use by another user.");
            }

            return ValidationResult.Success;
        }
    }


    public class ProfileViewModel
    {

        public string? id { get; set; }



        [Display(Name = "Avatar")]
        public IFormFile AvatarImage { get; set; }
        public string? imageURL { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        [UniqueUserName(ErrorMessage = "Username is already in use.")]
        public string UserName { get; set; }

        [StringLength(100, ErrorMessage = "Signature cannot be longer than 100 characters.")]
        public string Signature { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [UniqueEmail(ErrorMessage = "Email is already in use.")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string? NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "The password and confirmation password do not match.")]
        public string? ConfirmNewPassword { get; set; }
    }
}
