using EmployeeInfo.Entities.Models;

namespace EmployeeInfo.Repository.IRepository
{
    public interface IEmployeeRepository
    {
        Task<Employee?> GetByIdAsync(int id);
        Task<List<Employee>> GetAllAsync();
        Task<Employee> AddAsync(Employee entity);
        Task<Employee> EditAsync(Employee entity);
        Task<bool> DeleteAsync(int id);
        Task SaveChangesAsync();
    }
}
