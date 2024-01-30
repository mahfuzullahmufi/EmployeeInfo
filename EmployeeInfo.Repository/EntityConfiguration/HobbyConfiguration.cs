using EmployeeInfo.Entities.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace EmployeeInfo.Repository.EntityConfiguration
{
    public class HobbyConfiguration : IEntityTypeConfiguration<Hobby>
    {
        public void Configure(EntityTypeBuilder<Hobby> builder)
        {
            builder
                .HasOne(h => h.Employee)
                .WithMany(e => e.Hobbies)
                .HasForeignKey(h => h.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
