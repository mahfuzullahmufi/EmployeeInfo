using EmployeeInfo.Entities.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace EmployeeInfo.Repository.EntityConfiguration
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasOne(b => b.Employee)
            .WithOne(x => x.Address)
            .HasForeignKey<Address>(e => e.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
