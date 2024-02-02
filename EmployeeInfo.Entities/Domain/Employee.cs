using EmployeeInfo.Entities.Enum;
using LinqToDB.Mapping;

namespace EmployeeInfo.Entities.Domain
{
    [Table("Employees")]
    public class Employee
    {
        [PrimaryKey, Identity]
        public int EmployeeId { get; set; }
        [Column]
        public string EmployeeName { get; set; }
        [Column]
        public DesignationType EmployeeDesignation { get; set; }
        public Address Address { get; set; }
        public List<Hobby> Hobbies { get; set; }
        public List<EmployeeProject> EmployeeProjects { get; set; }
        public List<Project> Projects { get; set; }
        [Column]
        public double? Salary { get; set; }
        [Column]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
