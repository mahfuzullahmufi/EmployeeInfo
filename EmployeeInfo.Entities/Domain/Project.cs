namespace EmployeeInfo.Entities.Domain
{
    public class Project
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public List<EmployeeProject> EmployeeProjects { get; set; }
    }
}
