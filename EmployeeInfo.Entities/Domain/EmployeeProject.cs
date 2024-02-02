using LinqToDB.Mapping;

namespace EmployeeInfo.Entities.Domain
{
    [Table("EmployeeProjects")]
    public class EmployeeProject
    {
        [Column]
        public int EmployeeId { get; set; }
        [Association(ThisKey = "EmployeeId", OtherKey = "EmployeeId")]
        public Employee Employee { get; set; }
        [Column]
        public int ProjectId { get; set; }
        [Association(ThisKey = "ProjectId", OtherKey = "ProjectId")]
        public Project Project { get; set; }
    }
}
