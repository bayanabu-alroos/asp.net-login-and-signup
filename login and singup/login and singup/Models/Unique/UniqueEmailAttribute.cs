using login_and_singup.Data;
using System.ComponentModel.DataAnnotations;

namespace login_and_singup.Models.Uniqe
{
    public class UniqueEmailAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var _db = (AppDbContext)validationContext.GetService(typeof(AppDbContext));
            var email = value.ToString();

            var entity = validationContext.ObjectInstance;
            if (entity is Employee employee && employee.EmployeeId != 0) // Assuming 'User' is your model and it has an 'Id' property
            {
                // Check for uniqueness excluding the current record being edited
                if (_db.Employees.Any(u => u.Email == email && u.EmployeeId != employee.EmployeeId))
                {
                    return new ValidationResult("Email is already in use.");
                }
            }
            else
            {
                // Check for uniqueness for new records
                if (_db.Employees.Any(u => u.Email == email))
                {
                    return new ValidationResult("Email is already in use.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
