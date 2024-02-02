using LinqToDB.Mapping;

namespace EmployeeInfo.Entities.Domain
{
    [Table("Projects")]
    public class Project
    {
        [PrimaryKey,Identity]
        public int ProjectId { get; set; }
        [Column]
        public string ProjectName { get; set; }
        [Association(ThisKey = "ProjectId", OtherKey = "ProjectId")]
        public List<EmployeeProject> EmployeeProjects { get; set; }
    }
}
