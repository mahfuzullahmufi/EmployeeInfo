using LinqToDB.Mapping;

namespace EmployeeInfo.Entities.Domain
{
    [Table("Hobbies")]
    public class Hobby
    {
        [PrimaryKey, Identity]
        public int Id { get; set; }
        [Column]
        public string HobbyName { get; set; }
        [Column]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
