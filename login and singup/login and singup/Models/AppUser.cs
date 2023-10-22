using login_and_singup.Models.Enum;
using Microsoft.AspNetCore.Identity;

namespace login_and_singup.Models
{
    public class AppUser : IdentityUser
    {
        public string City { get; set; }    
        public Gender Gender { get; set; }

    }
}
