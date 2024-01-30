using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using EmployeeInfo.Entities.Domain;

namespace EmployeeInfo.Repository.EntityConfiguration
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            //builder.HasOne(b => b.Address)
            //.WithOne(x => x.Employee)
            //.HasForeignKey<Employee>(e => e.AddressId)
            //.OnDelete(DeleteBehavior.Cascade);
        }
    }
}
