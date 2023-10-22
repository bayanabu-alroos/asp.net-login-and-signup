using System.ComponentModel.DataAnnotations;

namespace login_and_singup.Models.ViewModel
{
    public class UsersInRoleViewModel
    {
        public string UserId {  get; set; }
        [Display(Name ="User Name")]
        public string UserName { get; set; }
        public bool IsSelected { get; set; }
    }
}
