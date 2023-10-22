using Microsoft.Build.Framework;

namespace login_and_singup.Models.ViewModel
{
    public class EditRoleViewModel
    {
        public EditRoleViewModel()
        {
            Users = new List<string>();
        }
        public string Id { get; set; }
        [Required]
        public required string RoleName { get; set; }
        public List<string> Users { get; set; }
    }

}
