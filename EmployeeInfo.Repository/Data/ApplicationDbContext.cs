using EmployeeInfo.Entities.Domain;
using EmployeeInfo.Repository.EntityConfiguration;
using Microsoft.EntityFrameworkCore;

namespace EmployeeInfo.Repository.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AddressConfiguration());
            modelBuilder.ApplyConfiguration(new HobbyConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeeProjectConfiguration());
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Hobby> Hobbies { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<EmployeeProject> EmployeeProjects { get; set; }

        
    }
}
