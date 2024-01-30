using EmployeeInfo.Entities.Domain;

namespace EmployeeInfo.Repository.IRepository
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        Task<Employee> GetEmployeeByIdWithProjects(int id);
        Task<List<Employee>> GetEmployeesWithProjects();
    }
}
