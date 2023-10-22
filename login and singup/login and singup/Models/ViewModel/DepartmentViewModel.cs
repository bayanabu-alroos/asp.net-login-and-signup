using System.ComponentModel.DataAnnotations;

namespace login_and_singup.Models.ViewModel
{
    public class DepartmentViewModel
    {
        public int DepartmentId { get; set; }
        [Display(Name = "Department Name")]
        [Required]
        public string DepartmentName { get; set; }
    }
}
