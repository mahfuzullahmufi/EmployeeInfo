using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeInfo.Entities.Domain
{
    public class Hobby
    {
        public int Id { get; set; }
        public string HobbyName { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
