using EmployeeInfo.Entities.Data;
using EmployeeInfo.Entities.Models;
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
            return await _dbSet.FindAsync(id);
        }

        public async Task<List<Employee>> GetAllAsync()
        {
            var result = _dbContext.Employees.OrderBy(e => e.EmployeeName).ThenBy(x => x.Salary);
            return result.ToList();
        }

        public async Task<Employee> AddAsync(Employee entity)
        {
            _dbSet.Add(entity);
            return entity;
        }

        public async Task<Employee> EditAsync(Employee entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public async Task DeleteAsync(Employee entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
