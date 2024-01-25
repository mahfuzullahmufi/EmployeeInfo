using EmployeeInfo.Entities.Enum;
using EmployeeInfo.Entities.Models;

namespace EmployeeInfo.Service.IService
{
    public interface IEmployeeService
    {
        Task<Employee?> GetByIdAsync(int id);
        Task<PagedViewModel<Employee>> GetAllAsync(int page, int pageSize);
        Task<bool> AddAsync(Employee entity);
        Task<bool> EditAsync(Employee entity);
        Task<bool> DeleteAsync(int id);
    }
}
