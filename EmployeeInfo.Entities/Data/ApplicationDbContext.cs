using EmployeeInfo.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeInfo.Entities.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }
    }
}
