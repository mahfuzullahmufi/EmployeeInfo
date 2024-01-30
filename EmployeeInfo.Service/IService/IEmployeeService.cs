using EmployeeInfo.Entities.Domain;

namespace EmployeeInfo.Service.IService
{
    public interface IEmployeeService
    {
        Task<Employee> GetByIdAsync(int id);
        Task<List<Employee>> GetAllAsync();
        Task<Employee> AddAsync(Employee entity);
        Task EditAsync(Employee entity);
        Task<bool> DeleteAsync(int id);
    }
}
