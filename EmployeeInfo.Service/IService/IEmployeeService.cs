using EmployeeInfo.Entities.Enum;
using EmployeeInfo.Entities.Models;
using EmployeeInfo.Service.VM;

namespace EmployeeInfo.Service.IService
{
    public interface IEmployeeService
    {
        Task<EmployeeVm?> GetByIdAsync(int id);
        Task<PagedViewModel<EmployeeTableVm>> GetAllAsync(int page, int pageSize);
        Task<bool> AddAsync(EmployeeVm model);
        Task<bool> EditAsync(EmployeeVm entity);
        Task<bool> DeleteAsync(int id);
    }
}
