using EmployeeInfo.Entities.Domain;
using EmployeeInfo.Repository.Data;
using EmployeeInfo.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace EmployeeInfo.Repository.Repository
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly Linq2DbDataConnection _connection;
        public EmployeeRepository(ApplicationDbContext context, Linq2DbDataConnection connection) : base(context)
        {
            _connection = connection;
        }

        public async Task<Employee> GetEmployeeByIdWithProjects(int id)
        {
            return await _db.Include(e => e.Address).Include(e => e.Hobbies).Include(e => e.EmployeeProjects).ThenInclude(y => y.Project).FirstOrDefaultAsync(x => x.EmployeeId == id);
        }

        public async Task<List<Employee>> GetEmployeesWithProjects()
        {
            try
            {
                //var result = _db.Include(e => e.Address).Include(e => e.Hobbies).Include(e => e.EmployeeProjects).ThenInclude(y => y.Project).OrderBy(e => e.EmployeeName).ThenBy(x => x.Salary);

                var result = await _connection.Employees.Include(x => x.Address).ToListAsync();
                return result.ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
            
        }
    }
}
