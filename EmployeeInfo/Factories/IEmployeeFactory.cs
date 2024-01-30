using EmployeeInfo.Web.Models;

namespace EmployeeInfo.Web.Factories
{
    public interface IEmployeeFactory
    {
        Task<PagedViewModel<EmployeeTableModel>> GetEmployeesWithProjects(int page, int pageSize);
        Task<EmployeeModel> GetEmployeeByIdWithProjects(int id);
        Task AddEmployeeAsync(EmployeeModel model);
        Task EditAsync(EmployeeModel model);
    }
}
