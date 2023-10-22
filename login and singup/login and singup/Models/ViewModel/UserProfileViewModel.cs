using System.ComponentModel.DataAnnotations;

namespace login_and_singup.Models.ViewModel
{
    public class UserProfileViewModel
    {
        public required string Email { get; set; }
  
        public required string Password { get; set; }
        public required string Phone { get; set; }
    }
}
