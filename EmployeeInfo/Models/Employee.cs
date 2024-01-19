using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EmployeeInfo.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Employee Name")]
        public string EmployeeName { get; set; }
        [Required]
        [DisplayName("Employee Designation")]
        public DesignationType EmployeeDesignation { get; set; }
        [Range(30000, 600000, ErrorMessage = "Salary must be between 30000 and 600000 !!")]
        public double? Salary { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
