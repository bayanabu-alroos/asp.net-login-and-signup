using login_and_singup.Models.Enum;
using login_and_singup.Models.Uniqe;
using System.ComponentModel.DataAnnotations;

namespace login_and_singup.Models.ViewModel
{
    public class EmployeeViewModel
    {
        public int EmployeeId { get; set; } //PK + Identity + not null + AutoNumbe
        [Display(Name = "Employee Name")]
        [Required(ErrorMessage = "The Employee Name field is required")]
        public string EmployeeName { get; set; }
        [Required(ErrorMessage = "The City field is required")]
        public string City { get; set; }
        [Required(ErrorMessage = "The E-mail field is required")]
        [EmailAddress]
        [UniqueEmail]
        public string Email { get; set; }
        [Required(ErrorMessage = "The Phone field is required")]
        public string Phone { get; set; }
        [Display(Name = "Hiring Data")]
        [Required(ErrorMessage = "The Hiring Data field is required")]
        [DataType(DataType.Date)]
        public DateTime HiringData { get; set; }
        [Required(ErrorMessage = "The Salary field is required")]
        public decimal Salary { get; set; }
        [Required(ErrorMessage = "The Select Gender  is required")]
        public Gender Gender { get; set; }
        [Required(ErrorMessage = "The  Postion field is required")]
        public string Postion { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int DepartmentId { get; set; }
    }
}
