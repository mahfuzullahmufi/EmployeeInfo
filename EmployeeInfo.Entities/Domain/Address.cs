using LinqToDB.Mapping;

namespace EmployeeInfo.Entities.Domain
{
    [Table("Addresses")]
    public class Address
    {
        [PrimaryKey, Identity]
        public int Id { get; set; }
        [Column]
        public string Street { get; set; }
        [Column]
        public string District { get; set; }
        [Column]
        public string Division { get; set; }
        [Column]
        public int EmployeeId { get; set; }
        [Association(ThisKey = "EmployeeId", OtherKey = "EmployeeId")]
        public Employee Employee { get; set; }
    }
}
