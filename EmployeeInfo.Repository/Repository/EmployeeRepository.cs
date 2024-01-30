using EmployeeInfo.Entities.Domain;
using EmployeeInfo.Repository.Data;
using EmployeeInfo.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace EmployeeInfo.Repository.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        protected readonly ApplicationDbContext _dbContext;
        private readonly DbSet<Employee> _dbSet;
        public EmployeeRepository(ApplicationDbContext context)
        {
            _dbContext = context;
            _dbSet = _dbContext.Set<Employee>();
        }

        public async Task<Employee?> GetByIdAsync(int id)
        {
            return await _dbContext.Employees.Include(e => e.Address).Include(e => e.Hobbies).Include(e => e.EmployeeProjects).ThenInclude(y => y.Project).FirstOrDefaultAsync(x => x.EmployeeId == id);
        }

        public async Task<List<Employee>> GetAllAsync()
        {
            var result = _dbContext.Employees.Include(e => e.Address).Include(e => e.Hobbies).Include(e => e.EmployeeProjects).ThenInclude(y => y.Project).OrderBy(e => e.EmployeeName).ThenBy(x => x.Salary);
            return result.ToList();
        }

        public async Task<Employee> AddAsync(Employee entity)
        {
            _dbSet.Add(entity);
            return entity;
        }

        public async Task<Employee> EditAsync(Employee entity)
        {
            _dbContext.Update(entity);
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _dbSet.FirstOrDefaultAsync(x => x.EmployeeId == id);
            if (entity != null) 
            {
                _dbSet.Remove(entity);
                await SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
