using login_and_singup.Models.Enum;
using System.ComponentModel.DataAnnotations;

namespace login_and_singup.Models.ViewModel
{
    public class RegisterViewModel
    {
        [Display(Name ="Email Address")]
        [Required(ErrorMessage ="Enter Email")]
        [EmailAddress]


        public required string EmailAddress { get; set; }
        [Required(ErrorMessage ="Enter Password")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*]).{8,}$", ErrorMessage = "Passwords must have at least one uppercase, one lowercase, one digit, one special character, and be at least 8 characters long.")]

        public required string Password { get; set; }
        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "Enter Confirm Password")]
        [Compare("Password",ErrorMessage ="Missing Matah")]
        [DataType(DataType.Password)]
        public required string ConfirmPassword { get; set; }
        [Required]
        public required string Phone { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public Gender Gender { get; set; }
    }
}
