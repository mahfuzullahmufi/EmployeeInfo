using EmployeeInfo.Entities.Enum;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EmployeeInfo.Web.Models
{
    public class EmployeeModel
    {
        public int EmployeeId { get; set; }
        [Required]
        [DisplayName("Employee Name")]
        public string EmployeeName { get; set; }
        [Required]
        [DisplayName("Employee Designation")]
        public DesignationType EmployeeDesignation { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string District { get; set; }
        [Required]
        public string Division { get; set; }
        [Required]
        public List<string> Hobbies { get; set; }
        [Required]
        public List<int> Projects { get; set; }
        [Range(30000, 600000, ErrorMessage = "Salary must be between 30000 and 600000 !!")]
        public double? Salary { get; set; }
    }
}
