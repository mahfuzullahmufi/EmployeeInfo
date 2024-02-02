using EmployeeInfo.Entities.Domain;

namespace EmployeeInfo.Repository.IRepository
{
    public interface ILinq2DbEmployeeRepo
    {
        Task<Employee> GetEmployeeByIdWithProjects(int id);
        Task<List<Employee>> GetEmployeesWithProjects();
        Task<bool> AddAsync(Employee entity);
        Task EditAsync(Employee entity);
        Task<bool> DeleteAsync(int id);
    }
}

