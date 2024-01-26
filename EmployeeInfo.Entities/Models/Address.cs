namespace EmployeeInfo.Entities.Models
{
    public class Address
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string District { get; set; }
        public string Division { get; set; }
        public Employee Employee { get; set; }
    }
}
