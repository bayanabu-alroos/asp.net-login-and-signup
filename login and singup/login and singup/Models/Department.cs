using System.ComponentModel.DataAnnotations;

namespace login_and_singup.Models
{
    public class Department:SharedProp
    {
        public int DepartmentId { get; set; }
        [Display(Name = "Department Name")]
        [Required]
        public string DepartmentName { get; set; }
        public List<Employee> Employees { get; set; }

        //public ICollection<Employee> Employees { get; set; }

    }
}
