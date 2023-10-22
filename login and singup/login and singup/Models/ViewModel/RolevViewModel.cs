using System.ComponentModel.DataAnnotations;

namespace login_and_singup.Models.ViewModel
{
    public class RoleViewModel
    {
        [Required(ErrorMessage ="Enter Role Name")]
        [Display(Name ="Role Name")]
        public required string RoleName { get; set; }
    }
}
