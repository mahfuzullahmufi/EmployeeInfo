using EmployeeInfo.Entities.Domain;
using EmployeeInfo.Repository.Data;
using EmployeeInfo.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace EmployeeInfo.Repository.Repository
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Employee> GetEmployeeByIdWithProjects(int id)
        {
            return await _db.Include(e => e.Address).Include(e => e.Hobbies).Include(e => e.EmployeeProjects).ThenInclude(y => y.Project).FirstOrDefaultAsync(x => x.EmployeeId == id);
        }

        public async Task<List<Employee>> GetEmployeesWithProjects()
        {
            var result = _db.Include(e => e.Address).Include(e => e.Hobbies).Include(e => e.EmployeeProjects).ThenInclude(y => y.Project).OrderBy(e => e.EmployeeName).ThenBy(x => x.Salary);
            return result.ToList();
        }
    }
}
