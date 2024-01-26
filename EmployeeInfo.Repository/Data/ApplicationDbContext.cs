using EmployeeInfo.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeInfo.Repository.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Hobby> Hobbies { get; set; }
        public DbSet<Project> Projects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Address)
                .WithOne(x => x.Employee)
                .HasForeignKey<Employee>(e => e.AddressId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Hobby>()
                .HasOne(h => h.Employee)
                .WithMany(e => e.Hobbies)
                .HasForeignKey(h => h.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Projects)
                .WithMany(p => p.Employees);
        }
    }
}
