using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace EmployeeInfo.Web.Models
{
    public class ProjectTableModel
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string Employees { get; set; }
    }
}
