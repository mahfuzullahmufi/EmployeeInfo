using EmployeeInfo.Web.Models;

namespace EmployeeInfo.Web.Factories.Interfaces
{
    public interface IEmployeeFactory
    {
        Task<PagedViewModel<EmployeeTableModel>> GetAllAsync(int page, int pageSize);
        Task<EmployeeModel> GetByIdAsync(int id);
        Task AddEmployeeAsync(EmployeeModel model);
        Task EditAsync(EmployeeModel model);
    }
}
