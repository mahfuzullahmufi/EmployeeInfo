using EmployeeInfo.Entities.Enum;
using EmployeeInfo.Entities.Models;

namespace EmployeeInfo.Service.VM
{
    public class EmployeeVm
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public DesignationType EmployeeDesignation { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; }
        public List<string> Hobbies { get; set; }
        public List<string> Projects { get; set; }
        public double? Salary { get; set; }
    }
}
