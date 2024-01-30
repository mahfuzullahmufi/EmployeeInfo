using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EmployeeInfo.Web.Models
{
    public class ProjectModel
    {
        public int ProjectId { get; set; }
        [Required]
        [DisplayName("Project Name")]
        public string ProjectName { get; set; }
        [Required]
        public List<int> Employees { get; set; }
    }
}
