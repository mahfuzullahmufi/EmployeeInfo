using EmployeeInfo.Entities.Enum;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EmployeeInfo.Entities.Domain
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        [Required]
        [DisplayName("Employee Name")]
        public string EmployeeName { get; set; }
        [Required]
        [DisplayName("Employee Designation")]
        public DesignationType EmployeeDesignation { get; set; }
        public Address Address { get; set; }
        public List<Hobby> Hobbies { get; set; }
        public List<EmployeeProject> EmployeeProjects { get; set; }
        [Range(30000, 600000, ErrorMessage = "Salary must be between 30000 and 600000 !!")]
        public double? Salary { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
