using EmployeeInfo.Entities.Enum;

namespace EmployeeInfo.Web.Models
{
    public class EmployeeTableModel
    {
        public int Id { get; set; }
        public string EmployeeName { get; set; }
        public DesignationType EmployeeDesignation { get; set; }
        public string Street { get; set; }
        public string District { get; set; }
        public string Division { get; set; }
        public string Hobbies { get; set; }
        public string Projects { get; set; }
        public double? Salary { get; set; }
    }
}
